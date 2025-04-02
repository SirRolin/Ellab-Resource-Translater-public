using Ellab_Resource_Translater.Objects;
using Ellab_Resource_Translater.Objects.Extensions;
using Ellab_Resource_Translater.Util;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ellab_Resource_Translater.Translators
{
    public class DBProcessorBase(TranslationService? TranslationService, ConnectionProvider? connProv, CancellationTokenSource source, Func<string, string> langToLocal, int systemEnum, int maxThreads = 32)
    {
        private readonly Config config = Config.Get();

        private readonly CancellationToken token = source.Token;

        public DatabaseTransactionHandler? dth;

        public void Run(string path, ListView view, Label progresText, Regex regex)
        {
            /// Local Functions to make the data transaction handler call more readable.
            Action<DbConnection, DbTransaction?> onTransactionStart(int systemEnum, Label progresText)
            {
                return (dbc, trans) =>
                {
                    // FetchData from Database that has entries in ChangedTranslations.
                    UpdateLocalFiles(path, view, progresText);

                    // Removing the relavant translations on the database.
                    progresText.Invoke(() => progresText.Text = "Clearing Database.");
                    var c = dbc.CreateCommand();
                    c.Transaction = trans;
                    c.CommandType = CommandType.Text;

                    ParamStringArray langs = new("@langs", config.languagesToTranslate, "EN");

                    c.CommandText = $@"
                            DELETE a FROM Translation a 
                            WHERE a.SystemEnum = {systemEnum} 
                            AND a.LanguageCode IN ({langs}) 
                            AND NOT a.ID in (Select ct.TranslationID from ChangedTranslation ct);";
                    // Add langs to the parameters so that we don't delete the once we're not working on.
                    langs.AddParam(c);

                    var rowsAffected = c.ExecuteNonQuery();
                    c.Dispose();

                    // Read, Translate, Write & prepare dth.
                    SetupTasks(path, view, progresText, regex);
                };
            }

            static Action<DataRow, Interfaces.IDBparameterable> addParameters()
            {
                return (row, paramable) =>
                {
                    paramable.AddParam(row, "Comment", DbType.String);
                    paramable.AddParam(row, "Key", DbType.String);
                    paramable.AddParam(row, "LanguageCode", DbType.String);
                    paramable.AddParam(row, "ResourceName", DbType.String);
                    paramable.AddParam(row, "Text", DbType.String);
                    paramable.AddParam(row, "IsTranlatedInValSuite", DbType.Boolean);
                    paramable.AddParam(row, "SystemEnum", DbType.Int32);
                };
            }

            string getResourceName(DataTable dt)
            {
                string output = "<Unknown>";
                // On Error DataTables are cleared, emptying them, but 
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Contains("ResourceName"))
                    output = dt.Rows[0]["ResourceName"].ToString() ?? "<Unknown>";
                return output;
            }

            // Setup a transaction handler, this makes it possible to cancel our work and revert back the changes on the database.
            dth = new(
                    source: source,
                    onTransactionStart: onTransactionStart(systemEnum, progresText),
                    // Key have to have quotes as "Key" is a keyword used in SQL
                    commandText: @"
                              INSERT INTO Translation 
                                  (Comment, ""Key"", LanguageCode, ResourceName, Text, IsTranlatedInValSuite, SystemEnum) 
                              VALUES 
                                  (@Comment, @Key, @LanguageCode, @ResourceName, @Text, @IsTranlatedInValSuite, @SystemEnum);",

                    addParameters: addParameters(),
                    inserters: Config.Get().insertersToUse);


            // Starts transfer to Database
            if (connProv != null && !token.IsCancellationRequested)
            {
                dth.StartCommands(connProv, progresText, view, getResourceName);
            }
        }

        /// <summary>
        /// Fetches Data from the database and updates local resource files.
        /// </summary>
        /// <param name="path">root path</param>
        /// <param name="view">the ViewList that shows which items are being processed atm.</param>
        /// <param name="progresText">The Label that is updated with the progres</param>
        /// <exception cref="NotImplementedException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0306:Simplify collection initialization", Justification = "it's a lie, collection initialization doesn't work for ConcurrentQueues")]
        private void UpdateLocalFiles(string path,
                                      ListView view,
                                      Label progresText)
        {
            // Update UI
            FormUtils.LabelTextUpdater(progresText, "Fetching changed translations from DB.");

            if (connProv != null && !token.IsCancellationRequested)
            {
                using DbConnection dbConn = connProv.Get();

                // Read from the DB
                using DbCommand command = dbConn.CreateCommand();

                /*// Only changes from changedTranslations
                command.CommandText = $@"WITH changedTranslationsLatest AS (
                                          SELECT
                                            *,
                                            ROW_NUMBER() OVER(PARTITION BY TranslationID ORDER BY ID DESC) AS ReverseRowNumber
                                          FROM ChangedTranslation
                                        )
                                        SELECT a.""Key"", a.ResourceName, a.LanguageCode, a.Comment, b.ChangedText, b.ID as ""ChangedID""
                                        FROM Translation a JOIN changedTranslationsLatest b ON a.ID = b.TranslationID WHERE b.ReverseRowNumber = 1 and a.SystemEnum = {systemEnum} and a.LanguageCode in ({});";
                //*/

                // Creating a object to easier apply the parameters to the command.
                ParamStringArray langs = new("@lang", config.languagesToTranslate, "EN");

                // Fetch all entries on the database that's not empty, prioritising the changedTranslations and is the right languages
                command.CommandText = $@"
                        WITH changedTranslationsLatest AS (
                            SELECT
                            *,
                            ROW_NUMBER() OVER(PARTITION BY TranslationID ORDER BY ID DESC) AS ReverseRowNumber
                            FROM ChangedTranslation
                        )

                        SELECT a.""Key"", a.ResourceName, a.LanguageCode, a.Comment, COALESCE(b.ChangedText, a.""Text"") AS ""ChangedText"", ISNULL(b.ID, -1) AS ""ChangedID""
                        FROM Translation a 
                        left JOIN changedTranslationsLatest b 
                        ON a.ID = b.TranslationID 
                        WHERE (b.ReverseRowNumber = 1 OR b.ID IS NULL)
                        AND COALESCE(b.ChangedText, a.""Text"") != '' 
                        AND a.SystemEnum = {systemEnum} 
                        AND a.LanguageCode IN ({langs});";
                // add languages to the parameters
                langs.AddParam(command);

                
                dbConn.WaitForOpen(() =>
                {
                    source.Cancel();
                    source.Token.ThrowIfCancellationRequested();
                });
                
                using DbDataReader changedTexts = command.ExecuteReader();

                // Load changes
                if (changedTexts.HasRows)
                {
                    // Quickly Extract Data to DataTables, so we can quantify the tables and close the connection.
                    ConcurrentQueue<DataTable> dataTables = [];
                    while (!changedTexts.IsClosed)
                    {
                        DataTable table = new();
                        table.Load(changedTexts);
                        dataTables.Enqueue(table);
                    }

                    dbConn.TryClose();

                    // local helper function that updates the GUI.
                    void myUpdate(string pretext, Ref<int> currentProgress, int maxProgresses)
                    {
                        FormUtils.LabelTextUpdater(progresText, pretext, Interlocked.Increment(ref currentProgress.value), " out of ", maxProgresses);
                    }

                    // Merging the Tables with column data so we can fetch it later.
                    // Reasoning for this is so we can multi-thread
                    ConcurrentTable<ChangeTranslationColumns> mergedTables = new(maxThreads, view, dataTables, myUpdate);

                    // Groups the data with the Dictionary.
                    // Reasoning is that if we don't do this we wouldn't be able to multi-thread it, due to opening files with readwrite access blocks other threads from doing the same on the same file.
                    // Even if it wouldn't block, it would likely cause problems or at the very least be less efficient as it would open the same files multiple times.
                    GroupChanges<MetaData<object?>, ChangeTranslationColumns> groupChanges = new(maxThreads,
                                                                                                 view,
                                                                                                 mergedTables,
                                                                                                 myUpdate,
                                                                                                 "Grouping Data on same Resource Files: ");


                    TruePathDict allResources = GetAllResourcePaths(path);

                    // Process Changes by the ResourceFile
                    UpdateLocalFilesFromGroupedData(maxThreads, path, view, allResources, groupChanges, myUpdate);

                    using DbCommand deleteCommand = dbConn.CreateCommand();
                    deleteCommand.CommandText = $@"
                            DELETE FROM ChangedTranslation
                            WHERE ChangedTranslation.TranslationID IN (
                                    SELECT ID FROM Translation 
                                    WHERE SystemEnum = {systemEnum} 
                                    AND LanguageCode IN ({langs})
                            );";
                    langs.AddParam(deleteCommand);

                    dbConn.WaitForOpen(() =>
                    {
                        source.Cancel();
                        source.Token.ThrowIfCancellationRequested();
                    });
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Dispose();
                    dbConn.CloseAsync();
                }
            }
        }

        private static void UpdateLocalFilesFromGroupedData(int maxThreads,
                                                            string path,
                                                            ListView view,
                                                            TruePathDict allResources,
                                                            GroupChanges<MetaData<object?>, ChangeTranslationColumns> changesToRegister,
                                                            Action<string, Ref<int>, int> myUpdate)
        {
            Ref<int> currentProgress = -1;
            const string TITLE = "Saving Changes: ";

            // ConcurrentDictionary can't be popped, so we have to have a queue of it's keys
            ConcurrentQueue<string> files = new(changesToRegister.Dict.Keys);
            int fileCount = files.Count;


            // Initial Update of UI
            myUpdate(TITLE, currentProgress, fileCount);
            void processChanges(string rootPath, string transDictKey)
            {
                
                var resourcePath = Path.Combine(rootPath, transDictKey);

                // In Case the english and language files are not the same casing
                if (allResources.Dict.TryGetValue(resourcePath.ToLowerInvariant(), out string? truePath))
                    resourcePath = truePath;

                // Load Local data so we don't lose data that wasn't overriden
                Dictionary<string, MetaData<object?>> translations = ResourceHandler.ReadResource<object?>(resourcePath);

                // Override in Memory
                var changes = changesToRegister.Dict[transDictKey];
                changes.ForEach(change =>
                {
                    if (translations.TryGetValue(change.key, out MetaData<object?>? oldtrans))
                    {
                        oldtrans.value = change.value;
                        oldtrans.comment = change.comment;
                    }
                    else
                    {
                        translations.Add(change.key, change);
                    }
                });

                // Save to Local Data
                ResourceHandler.WriteResource(resourcePath, translations);
            }
            ExecutionHandler.Execute(maxThreads, fileCount, (int i) =>
            {
                while (files.TryDequeue(out var resourceName))
                {
                    FormUtils.ShowOnListWhileProcessing(
                        onStart: () => myUpdate(TITLE, currentProgress, fileCount),
                        listView: view,
                        processName: i + ") Fetching Data...",
                        process: () => processChanges(path, resourceName));
                }
            });
        }

        private void SetupTasks(string path, ListView view, Label progresText, Regex regex)
        {
            // Tracks resources done - Starts at -1 so that we can call it to get the right format to start with.
            int currentProcessed = -1;
            TruePathDict allResources = GetAllResourcePaths(path);
            var englishQueuedFiles = new ConcurrentQueue<string>(allResources.Dict.Select(x => x.Value).Where(x => regex.IsMatch(x)));

            int maxProcesses = englishQueuedFiles.Count;

            // Doing this so we don't have to pass both a int ref and a Label ref
            void updateProgresText()
            {
                var progress = Interlocked.Increment(ref currentProcessed);
                FormUtils.LabelTextUpdater(progresText, "Preparing ", progress, " out of ", maxProcesses);
            }
            updateProgresText();

            void onFailing(AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        englishQueuedFiles.Clear();
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            // Execution Time
            ExecutionHandler.TryExecute(maxThreads, allResources.Dict.Count,
                    action: (i) => ProcessQueue(rootPathLength: path.Length,
                                                               queue: englishQueuedFiles,
                                                               existingResources: allResources,
                                                               update: updateProgresText,
                                                               listView: view),
                    onFailing: (Action<AggregateException>)onFailing
                );
        }

        private static TruePathDict GetAllResourcePaths(string path)
        {
            return new(Directory.GetFiles(path, "*.resx", SearchOption.AllDirectories));
        }

        private void ProcessQueue(int rootPathLength, ConcurrentQueue<string> queue, TruePathDict existingResources, Action update, ListView listView)
        {
            while (!source.IsCancellationRequested && queue.TryDequeue(out var resource))
            {
                void TryTranslateResource()
                {
                    try
                    {
                        TranslateResource(existingResources, resource, rootPathLength);
                    }
                    catch (AggregateException ae)
                    {
                        if (!source.IsCancellationRequested)
                        {
                            source.Cancel();
                            Debug.WriteLine(ae.Message);
                            MessageBox.Show("Translations used up for now. Try again later.");
                        }
                    }
                }
                FormUtils.ShowOnListWhileProcessing(rootPathLength, update, listView, resource, TryTranslateResource);
            }
        }

        private void TranslateResource(TruePathDict existingFiles, string resource, int pathLength)
        {
            // Translations work
            var langs = config.languagesToTranslate.ToArray();
            TranslationLangDictionary<object?> translations = ResourceHandler.GetAllLangResources(existingFiles, resource, langToLocal, langs, Config.Get().languagesToAiTranslate, TranslationService);

            // Save Translations
            foreach (var item in translations.Dict)
            {
                if (!item.Key.Equals("EN", StringComparison.OrdinalIgnoreCase))
                    ResourceHandler.WriteResource(Path.ChangeExtension(resource, $"{langToLocal(item.Key)}.resx"), item.Value);
            }

            // Try to write to the Database
            WriteToDatabase(pathLength, resource, translations);
        }


        private void WriteToDatabase(int pathLength, string resource, TranslationLangDictionary<object?> translations)
        {
            if (dth != null)
            {
                // Filter to only be strings as others don't need translating
                TranslationLangDictionary<string> toUpload = new(translations.Dict.Select(langDict =>
                    new KeyValuePair<string, Dictionary<string, MetaData<string>>>(
                        langDict.Key,
                        langDict.Value.FilterTo<string>().FilterKeyStartsOut("$", ">>$")
                        ))
                    .ToDictionary());

                // Get the missing string entries that hasn't been translated and add them to the datatables to upload.
                foreach (string lang in toUpload.Dict.Keys)
                {
                    if (!lang.Equals("EN", StringComparison.OrdinalIgnoreCase))
                    {
                        var missing = ResourceHandler.GetMissingStringEntries(translations, lang, true);
                        missing = missing.FilterKeyStartsOut("$", ">>$");
                        foreach (var item in missing)
                        {
                            toUpload.Dict[lang].Add(item.key, item);
                        }
                    }
                }

                DataTable dataTable = CreateDataTable<string>(pathLength, resource, toUpload, systemEnum);
                if (dataTable.Rows.Count > 0)
                    dth.AddInsert(dataTable);
            }
        }

        private static DataTable CreateDataTable<T>(int pathLength, string resource, TranslationLangDictionary<T> translations, int systemEnum)
        {
            DataTable dataTable = new();
            dataTable.Columns.Add("ID", typeof(long));
            dataTable.Columns.Add("Comment", typeof(string));
            dataTable.Columns.Add("Key", typeof(string));
            dataTable.Columns.Add("LanguageCode", typeof(string));
            dataTable.Columns.Add("ResourceName", typeof(string));
            dataTable.Columns.Add("Text", typeof(string));
            dataTable.Columns.Add("IsTranlatedInValSuite", typeof(bool)); // The Spelling Error is on the Database
            dataTable.Columns.Add("SystemEnum", typeof(int));
            foreach (var item in translations.Dict)
            {
                string language = item.Key;
                // add language string (if not english), then cut rootPath away.
                //string resourceName = (item.Key.Equals("EN", StringComparison.OrdinalIgnoreCase) ? resource : Path.ChangeExtension(resource, $".{langToLocal(item.Key)}.resx"))[(pathLength + 1)..];
                // Changed due to the WebTranslator comparing on resourceName
                string resourceName = resource[(pathLength + 1)..];

                foreach (var value in item.Value)
                {
                    // it's only useful to send strings up
                    if (value.Value.value is not string)
                        continue;
                    string comment = value.Value.comment;
                    string key = value.Key;
                    object text = value.Value.value; // Stonks
                    bool IsTranlateInValSuite = !string.IsNullOrEmpty(text.ToString());

                    dataTable.Rows.Add(null, // ID, should autogenerate
                                        comment,
                                        key,
                                        language,
                                        resourceName,
                                        text,
                                        IsTranlateInValSuite,
                                        systemEnum
                                        );
                }
            }

            return dataTable;
        }
    }
}