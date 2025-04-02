using Azure;
using Azure.AI.Translation.Text;
using Ellab_Resource_Translater.Forms;
using Ellab_Resource_Translater.Structs;
using Ellab_Resource_Translater.Translators;
using Ellab_Resource_Translater.Util;
using Newtonsoft.Json;
using System.Data.Common;

namespace Ellab_Resource_Translater
{
    public partial class MainForm : Form
    {
        private int setup = 0;
        private bool batching = false;
        private ConnectionProvider? connProv;
        private TranslationService? translationService;
        internal const string CONNECTION_SECRET = "EllabResourceTranslator:dbConnection";
        internal const string AZURE_SECRET = "EllabResourceTranslator:azure";
        private CancellationTokenSource? cancelTSource;

        private const string IS_CONNECTING = "Connecting...";
        private const string CAN_CONNECT = "Can Connect";
        private const string DB_CONN_DEFAULT = "Database Connection Status";


        public MainForm()
        {
            InitializeComponent();
            TooltipNormal.SetToolTip(translationLabel, "Which languages to transfer the english versions to");
        }

        private void UpdateConnectionStatus(string connectionStringState)
        {
            connectionStatus.Invoke(() => connectionStatus.Text = string.Concat("DB ", connectionStringState));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Config.ExistsOnDisk())
            {
                var answer = MessageBox.Show(this, "It seems to be your first time running this program.\nDo you want to run the setup?",
                                             "First Time Setup.",
                                             MessageBoxButtons.YesNo);
                if (answer.Equals(DialogResult.Yes))
                {
                    OpenSettings();
                    OpenDBSetup();
                    OpenAzureSetup();
                }
            }


            var config = Config.Get();
            var languagePairs = Config.DefaultLanguages();
            var checkitems = config.languagesToTranslate;

            setup++;
            FormUtils.LoadCheckboxListLocalised(
                list: checkitems,
                checkedListBox: translationCheckedListBox,
                localiser: languagePairs
                );

            TryConnectDB();
            TryConnectAzure();
            Config.AssignSizeSetting(this, (s) => config.MainWindowSize = s, config.MainWindowSize);
            setup--;
        }

        public Task TryConnectDB()
        {
            return Task.Run(async () =>
            {
                // Avoid trying to refresh while still connecting.
                RefreshConnectionButton.Invoke(() => RefreshConnectionButton.Enabled = false);

                // cleanup
                connProv?.Dispose();

                string? connString = SecretManager.GetUserSecret(CONNECTION_SECRET);

                if (connString == null)
                {
                    connString = DatabaseSelecterForm.DEFAULTSERVERSTRING;
                    SecretManager.SetUserSecret(CONNECTION_SECRET, connString);
                }
                    


                // Debugging
                //RefreshConnectionButton.Invoke(() => MessageBox.Show(this, dbConn.Replace(";", ";\n")));


                if (connString != null)
                {
                    connProv = new(connString);
                    UpdateConnectionStatus(IS_CONNECTING);
                    
                    try
                    {
                        using DbConnection conn = connProv.Get();
                        await conn.OpenAsync();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            UpdateConnectionStatus(CAN_CONNECT);
                        }
                        await conn.CloseAsync();
                        return;
                    }
                    catch (Exception ex)
                    {
                        // In case we try connect to a new connection while still trying to connect.
                        if (ex is TaskCanceledException)
                            return;

                        UpdateConnectionStatus(ex.Message);
                        connProv?.Dispose();

                        // If we can't connect (MSSQL, MYSQL and PostGres)
                        if (ex.InnerException?.Message == "No such host is known.")
                        {
                            using var _ = Task.Run(() =>
                            {
                                var answer = GetBlockingInput("Couldn't Connect, do you want to open Database Selecter?", "Couldn't Connect", MessageBoxButtons.YesNo);
                                if (answer.HasFlag(DialogResult.Yes))
                                {
                                    this.Invoke(() => OpenDBSetup());
                                }
                            });
                        }
                    }
                }
                else
                {
                    UpdateConnectionStatus("Need Setup:");
                }

                // Reenabling the refresh
                RefreshConnectionButton.Invoke(() => RefreshConnectionButton.Enabled = true);
            });
        }

        public Task TryConnectAzure()
        {
            return Task.Run(() =>
            {
                AzureCredentials? azureCreds;

                try
                {
                    string? jsonCreds = SecretManager.GetUserSecret(AZURE_SECRET);
                    if (jsonCreds != null)
                        azureCreds = JsonConvert.DeserializeObject<AzureCredentials>(jsonCreds);
                    else
                    {
                        AzureConnectionStatus.Invoke(() =>
                        {
                            AzureConnectionStatus.Text = "Azure, Need Credentials";
                            OpenAzureSetup();
                        });
                        return;
                    }
                }
                catch
                {
                    AzureConnectionStatus.Invoke(() =>
                    {
                        AzureConnectionStatus.Text = "Azure, Error with stored Credentials";
                        OpenAzureSetup();
                    });
                    return;
                }


                // Avoid trying to refresh while still connecting.
                RefreshAzureButton.Invoke(() => RefreshAzureButton.Enabled = false);

                if (azureCreds.HasValue)
                {
                    AzureConnectionStatus.Invoke(() => AzureConnectionStatus.Text = "Azure Connecting...");
                    try
                    {
                        translationService = new(creds: azureCreds.Value.Key, uri: new Uri(azureCreds.Value.URI), region: azureCreds.Value.Region)
                        {
                            msWaitTime = Config.Get().checkDelay
                        };
                        if (!translationService.CanReachAzure().Result)
                            throw new Exception("Cannot Reach Azure");

                        AzureConnectionStatus.Invoke(() => AzureConnectionStatus.Text = "Azure Connected");
                    }
                    catch (Exception ex)
                    {
                        AzureConnectionStatus.Invoke(() => AzureConnectionStatus.Text = ex.Message);
                        RefreshAzureButton.Invoke(() => RefreshAzureButton.Enabled = true);
                        return;
                    }
                    return;
                }
                else
                {
                    AzureConnectionStatus.Invoke(() =>
                    {
                        AzureConnectionStatus.Text = "Azure, Need Credentials";
                        OpenAzureSetup();
                    });
                }

                // Reenabling the refresh
                RefreshAzureButton.Invoke(() => RefreshAzureButton.Enabled = true);
            });
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            connProv?.Dispose();
            Config.Save();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private static void OpenSettings()
        {
            var setting = new Settings();
            setting.BringToFront();
            setting.ShowDialog();
        }

        private void TranslationCheckedListBox_CheckChanged(object sender, EventArgs e)
        {
            // While Loading I don't want this to run
            if (setup > 0)
                return;

            var config = Config.Get();
            var languagePairs = Config.DefaultLanguages();

            FormUtils.SaveCheckBoxListChangeLocalised(
                list: config.languagesToTranslate,
                checkedListBox: translationCheckedListBox,
                localiser: languagePairs);

        }

        private async void ValSuite_Initiation(object sender, EventArgs e)
        {
            AbleControls(false);

            if (Config.Get().languagesToAiTranslate.Count == 0 || translationService != null)
            {
                cancelTSource = new CancellationTokenSource();

                await Task.Run(() => ValSuite_Init(translationService, cancelTSource));

                try
                {
                    progressTitle.Invoke(() => progressTitle.Text = cancelTSource.IsCancellationRequested ? "Request Cancelled" : "Request Done");
                } catch (InvalidOperationException)
                {
                    // Closed form before process finished.
                }

                if (!cancelTSource.IsCancellationRequested && Config.Get().closeOnceDone)
                    Close();

                cancelTSource.Dispose();
            }
            else
            {
                MessageBox.Show(this, @"Azure not connected, you can either:
    1) Setup Azure in the Azure button at the buttom.
    2) Disable AI Translation for all groups in Settings");
            }
            AbleControls(true);
        }

        public void ValSuite_Init(TranslationService? transServ, CancellationTokenSource source)
        {
            progressTitle.Invoke(() => progressTitle.Text = "Val Suite");
            var config = Config.Get();
            if (!CanDBConnecting())
            {
                ShowBlockingOkMessage("DB Connection Failed, try again later", "DB Connection Failed");
                return;
            }
            if (config.ValPath != null && config.ValPath != "" && !source.IsCancellationRequested)
            {
                try
                {
                    ValSuite val = new(transServ, connProv, source);
                    val.Run(config.ValPath, progressListView, progressTracker);
                }
                catch (OperationCanceledException){}

            } else if (batching == true)
            {
                DialogResult shouldWeContinue = GetBlockingInput("Check ValSuite path in Settings.\nShould we continue with the rest?", "Val suite Path Missing!", MessageBoxButtons.YesNo);
                if (shouldWeContinue != DialogResult.Yes) source.Cancel();
            }
            else
            {
                ShowBlockingOkMessage("Check ValSuite path in Settings", "Val suite path Missing");
            }
        }

        private DialogResult ShowBlockingOkMessage(string text, string title)
        {
            return this.Invoke(() => {
                Enabled = false;
                var output = MessageBox.Show(this, text, title, MessageBoxButtons.OK);
                Enabled = true;
                return output;
            });            
        }

        private DialogResult GetBlockingInput(string text, string title, MessageBoxButtons but)
        {
            return this.Invoke(() => {
                Enabled = false;
                var output = MessageBox.Show(this, text, title, but);
                Enabled = true;
                return output;
            });
        }

        private async void EMSuite_Initiation(object sender, EventArgs e)
        {
            AbleControls(false);

            if (Config.Get().languagesToAiTranslate.Count == 0 || translationService != null)
            {
                cancelTSource = new CancellationTokenSource();

                await Task.Run(() => EMSuite_Init(translationService, cancelTSource));

                progressTitle.Invoke(() => progressTitle.Text = "Done");

                if (!cancelTSource.IsCancellationRequested && Config.Get().closeOnceDone)
                    Close();

                cancelTSource.Dispose();
            }
            else
            {
                MessageBox.Show(@"Azure not connected, you can either:
    1) Setup Azure in the Azure button at the buttom.
    2) Disable AI Translation for all groups in Settings");
            }

            AbleControls(true);
        }

        /// <summary>
        /// Disables the Buttons so that we don't Instantiate multiple tranlations at once
        /// </summary>
        /// <param name="enable">enabled or not - reversed for Cancel button.</param>
        private void AbleControls(bool enable)
        {
            ValSuiteButton.Enabled = enable;
            EMSuiteButton.Enabled = enable;
            EMandValButton.Enabled = enable;
            SettingsButton.Enabled = enable;
            DBConnectionSetup.Enabled = enable;
            AzureSettingsSetup.Enabled = enable;
            RefreshAzureButton.Enabled = enable;
            RefreshConnectionButton.Enabled = enable;
            translationCheckedListBox.Enabled = enable;
            CancellationButton.Enabled = !enable;
        }

        public void EMSuite_Init(TranslationService? transServ, CancellationTokenSource source)
        {
            progressTitle.Invoke(() => progressTitle.Text = "EM Suite");
            var config = Config.Get();
            if (!CanDBConnecting())
            {
                ShowBlockingOkMessage("DB Connection Failed, try again later", "DB Connection Failed");
                return;
            }

            if (config.EMPath != null && config.EMPath != "" && !source.IsCancellationRequested)
            {
                try
                {
                    EMSuite emsuite = new(transServ, connProv, source);
                    emsuite.Run(config.EMPath, progressListView, progressTracker);
                }
                catch (OperationCanceledException){}
            } else if (batching == true)
            {
                DialogResult shouldWeContinue = GetBlockingInput("Check EMSuite path in Settings.\nShould we continue with the rest?", "EM suite Path Missing!", MessageBoxButtons.YesNo);
                if (shouldWeContinue != DialogResult.Yes) source.Cancel();
            }
            else
            {
                ShowBlockingOkMessage("Check EMSuite path in Settings", "EM suite path Missing");
            }
        }

        private async void EMandValButton_Click(object sender, EventArgs e)
        {
            AbleControls(false);

            batching = true;

            if (Config.Get().languagesToAiTranslate.Count == 0 || translationService != null)
            {
                cancelTSource = new CancellationTokenSource();
                if (!cancelTSource.IsCancellationRequested)
                    await Task.Run(() => EMSuite_Init(translationService, cancelTSource));
                if (!cancelTSource.IsCancellationRequested) // in case we want to cancel after finding out EMsuite didn't have a value
                    await Task.Run(() => ValSuite_Init(translationService, cancelTSource));

                if (!cancelTSource.IsCancellationRequested && Config.Get().closeOnceDone)
                    Close();

                cancelTSource.Dispose();
            }
            else
            {
                ShowBlockingOkMessage(@"Azure not connected, you can either:
    1) Setup Azure in the Azure button at the buttom.
    2) Disable AI Translation for all groups in Settings", "No Azure");
            }
            progressTitle.Invoke(() => progressTitle.Text = "Done");
            batching = false;

            AbleControls(true);
        }

        private void DBConnectionSetup_Click(object sender, EventArgs e)
        {
            OpenDBSetup();
        }

        private void OpenDBSetup()
        {
            DatabaseSelecterForm DBSform = new(this);
            DBSform.BringToFront();
            DBSform.ShowDialog();
        }

        private void AzureSettingsSetup_Click(object sender, EventArgs e)
        {
            OpenAzureSetup();
        }

        private void OpenAzureSetup()
        {
            AzureForm azureform = new(this);
            azureform.BringToFront();
            azureform.ShowDialog();
        }

        private void RefreshConnectionButton_Click(object sender, EventArgs e)
        {
            TryConnectDB();
        }

        private void RefreshAzureButton_Click(object sender, EventArgs e)
        {
            TryConnectAzure();
        }

        private void CancellationButton_Click(object sender, EventArgs e)
        {
            cancelTSource?.Cancel();
        }

        private bool CanDBConnecting()
        {
            // if it can't connect or isn't trying, try.
            if (!new[]{ "DB " + CAN_CONNECT, "DB " + IS_CONNECTING, DB_CONN_DEFAULT }.Contains(connectionStatus.Text))
            {
                TryConnectDB();
            }

            int delay = Config.Get().checkDelay;
            // Wait for the DB to no longer be connecting
            while (new[] {"DB " + IS_CONNECTING, DB_CONN_DEFAULT }.Contains(connectionStatus.Text))
            {
                Task.Delay(delay).Wait();
            }

            // Return if it can connect.
            return !connProv?.isDisposed() ?? false && connectionStatus.Text.Equals("DB " + CAN_CONNECT);
        }
    }
}
