using Ellab_Resource_Translater.Structs;
using Ellab_Resource_Translater.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ellab_Resource_Translater.Forms
{
    public partial class AzureForm : Form
    {
        private readonly MainForm parentMainForm;
        public AzureForm(MainForm mainForm)
        {
            this.parentMainForm = mainForm;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string key = AzureKey.Text;
            string URI = AzureURI.Text;
            string region = AzureRegion.Text;

            AzureCredentials creds = new(key, URI, region);

            SecretManager.SetUserSecret(MainForm.AZURE_SECRET, JsonConvert.SerializeObject(creds));
            parentMainForm.TryConnectAzure();
            this.Close();
        }
    }
}
