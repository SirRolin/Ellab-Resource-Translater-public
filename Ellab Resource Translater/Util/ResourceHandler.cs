using Ellab_Resource_Translater.Objects;
using Ellab_Resource_Translater.Objects.Extensions;
using System.Collections;
using System.Linq;
using System.Resources;

namespace Ellab_Resource_Translater.Util
{
    public static class ResourceHandler
    {
        /// <summary>
        /// Reads the resource file (.resx) and returns a dictionary of the entries with the key as the dictionary key.
        /// </summary>
        /// <typeparam name="Type">Type of value, if you plan to write back to the resource file, this should be <see cref="object"/>?, otherwise it filters to only the correct types.</typeparam>
        /// <param name="path">path of the resource</param>
        /// <returns>Dictionary with key, <see cref="MetaData"/>, which is a (key, value, comment) object, that can implicitly be converted to a <see cref="ResXDataNode"/>.</returns>
        public static Dictionary<string, MetaData<Type>> ReadResource<Type>(string path)
        {
            Dictionary<string, MetaData<Type>> trans = [];
            using (ResXResourceReader resxReader = new(path))
            {
                using ResXResourceReader resxCommentReader = new(path);
                // Switches to reading metaData instead of values, can't have both, which we need for comments
                resxCommentReader.UseResXDataNodes = true;

                // Found out that some files are simply broken which will cause this to throw an error when it reaches the end of the file.
                try
                {
                    var enumerator = resxCommentReader.GetEnumerator();
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        string key = entry.Key.ToString() ?? string.Empty;
                        string comment;

                        // Since we have 2 readers of the same File, we can iterate over them synced by calling MoveNext only once per loop
                        if (enumerator.MoveNext())
                        {
                            ResXDataNode? current = (ResXDataNode?)((DictionaryEntry)enumerator.Current).Value;
                            comment = current?.Comment ?? string.Empty;
                        }
                        else
                            comment = string.Empty;

                        if (entry.Value is Type value)
                            trans.Add(key, new MetaData<Type>(key, value, comment));
                    }
                }
                catch
                {
                }
            }
            return trans;
        }

        /// <summary>
        /// Writes the <paramref name="data"/> into <paramref name="path"/> resource file.
        /// </summary>
        /// <remarks>
        /// this might throw an IO error if path is incorrect or access is blocked.
        /// </remarks>
        /// <param name="path">File Path.</param>
        /// <param name="data"></param>
        public static void WriteResource(string path, Dictionary<string, MetaData<object?>> data)
        {
            using ResXResourceWriter resxWriter = new(path);
            foreach (var entry in data)
            {
                entry.Value.WriteToResourceWriter(resxWriter);
            }
        }
        /// <summary>
        /// Reads resource files of the english and the languages in <paramref name="langs"/>.
        /// Then fills the missing entried of <paramref name="langs"/> out with the english once.
        /// </summary>
        /// <remarks>
        /// If you want to also translate the missing entries use the other overloaded function, where you include a <see cref="TranslationService"/> as the last parameter.
        /// </remarks>
        /// <param name="existing">a HashSet of all the resource files considered "existing" in the root folder.</param>
        /// <param name="resource">Full Path to the english resource.</param>
        /// <param name="langs">Languagues other than english to also read and prepare.<br/>Upper Case national short form. ex: "EN", "DE", "ZH".</param>
        /// <param name="GetLangStr">Gets the file surfix from language to check for language files.</param>
        /// <param name="langsToAi">if the entry doesn't exist or is empty, and language doesn't exist in this array, it'll fill in the english entry for it.</param>
        /// <returns>Level 1 Key is the language, level 2 Key is the Entries Key.</returns>
        public static TranslationLangDictionary<object?> GetAllLangResources(TruePathDict existing, string resource, Func<string, string> GetLangStr, string[] langs)
        {
            // To store the Data of each language.
            TranslationLangDictionary<object?> translations = new([]);
            // Retrieve the English information
            translations.Dict.Add("EN", ResourceHandler.ReadResource<object?>(resource));
            foreach (var lang in langs)
            {
                string langPath = Path.ChangeExtension(resource, $"{GetLangStr(lang)}.resx").ToLower();
                // Setup the Translations
                if (existing.Dict.TryGetValue(langPath, out string? truePath))
                    translations.Dict.Add(lang, ResourceHandler.ReadResource<object?>(truePath));
                else
                    translations.Dict.Add(lang, []);
            }

            return translations;
        }

        /// <summary>
        /// Reads resource files of the english and the languages in <paramref name="langs"/>.
        /// Then fills the missing entried of <paramref name="langs"/> out with the english once.
        /// </summary>
        /// <remarks>
        /// If you want to also translate the missing entries use the other overloaded function, where you include a <see cref="TranslationService"/> as the last parameter.
        /// </remarks>
        /// <param name="existing">a HashSet of all the resource files considered "existing" in the root folder.</param>
        /// <param name="resource">Full Path to the english resource.</param>
        /// <param name="langs">Languagues other than english to also read and prepare.<br/>Upper Case national short form. ex: "EN", "DE", "ZH".</param>
        /// <param name="langsToAi">if the entry doesn't exist or is empty, and language doesn't exist in this array, it'll fill in the english entry for it.
        /// <br/>If the entry just doesn't exist or is empty, it'll be translated with the TranslationService.</param>
        /// <param name="translationService">The service that uses ai to translate the entry values.</param>
        /// <returns>Level 1 Key is the language, level 2 Key is the Entries Key.</returns>
        public static TranslationLangDictionary<object?> GetAllLangResources(TruePathDict existing, string resource, Func<string, string> GetLangStr, string[] langs, IEnumerable<string> langsToAi, TranslationService? translationService)
        {
            var output = GetAllLangResources(existing, resource, GetLangStr, langs);
            // Only get the once that are in both arrays/enumerables.
            var translatelangs = langs.Intersect(langsToAi);
            foreach (var lang in translatelangs)
            {
                // AI Translation
                ResourceHandler.TranslateMissingValuesToLang(output, lang, translationService);
            }
            return output;
        }


        /// <summary>
        /// Translates missing entries of the language provided.
        /// Outputs it into the same Dictionary.
        /// </summary>
        /// <remarks>
        /// If you already are using GetAllLangResources, consider adding the <see cref="TranslationService"/> to that call instead of doing it manually.
        /// </remarks>
        /// <param name="translations">Level 1 Key is the language, level 2 Key is the Entries Key.</param>
        /// <param name="lang">Which Language should we translate?</param>
        /// <param name="TranslationService">The service that uses ai to translate the entry values.</param>
        public static void TranslateMissingValuesToLang(TranslationLangDictionary<object?> translations, string lang, TranslationService? TranslationService)
        {
            List<MetaData<string>> missingTranslations = GetMissingStringEntries(translations, lang, false);

            // Nothing to translate? return
            if (missingTranslations.Count == 0 || TranslationService == null)
                return;

            // Get missing translation values in english as a Reverse Dictionary
            // Filter Weird once away
            // GroupBy so that dublicate values doesn't break as it becomes a key
            // Another Filter to remove the once that doesn't have a text in english (can't translate empty string)
            Dictionary<string, MetaData<string>[]> kvp = missingTranslations
                .FilterKeyStartsOut("$", ">>$")
                .GroupBy(keySelector: x => translations.Dict["EN"][x.key].value as string ?? string.Empty, x => x)
                .Where(k => !k.Key.Equals(string.Empty))
                .ToDictionary(g => g.Key, g => g.ToArray());

            // Getting the values, which as this point is the keys
            string[] textsToTranslate = [.. kvp.Keys];

            if (textsToTranslate.Length > 0 && TranslationService != null)
            {
                var response = TranslationService.TranslateTextAsync(textsToTranslate, lang).Result;
                foreach (var (source, translation) in response)
                {
                    var itemST = source;
                    var transes = kvp[itemST];
                    foreach (MetaData<string> transItem in transes)
                    {
                        string? text = translation[0];
                        if (text != null)
                        {
                            transItem.value = text;
                            transItem.comment = String.Join("\n", transItem.comment, "#AI");
                        }
                        else if (translations.Dict["EN"][itemST].comment is string englishComment) // Shouldn't ever be false, but if it is, we avoid the error.
                        {
                            transItem.value = string.Empty;
                            transItem.comment = String.Join("\n", transItem.comment, englishComment);
                        }
                        
                        translations.Dict[lang].Add(transItem.key, new MetaData<object?>(transItem.key, transItem.value, transItem.comment));
                    }
                }
            }
        }

        public static List<MetaData<string>> GetMissingStringEntries(TranslationLangDictionary<object?> translations, string lang, bool EMPTY_VALUES)
        {
            // Find missing translation keys
            List<MetaData<string>> missingTranslations = [];
            foreach (string entry in translations.Dict["EN"].Keys)
            {
                bool alreadyTranslated = translations.Dict[lang].TryGetValue(entry, out MetaData<object?>? trans) && (trans.value is string strVal && !string.IsNullOrEmpty(strVal));
                if (!alreadyTranslated && translations.Dict["EN"][entry].value is string enValue)
                {
                    string value = EMPTY_VALUES ? string.Empty : enValue;
                    var comment = translations.Dict["EN"][entry].comment;

                    // Add it to the Languages Dictionary
                    missingTranslations.Add(new MetaData<string>(entry, value, comment, lang));
                }
            }

            return missingTranslations;
        }
    }
}
