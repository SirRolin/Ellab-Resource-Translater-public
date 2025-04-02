using Ellab_Resource_Translater.Objects;
using Ellab_Resource_Translater.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Config = Ellab_Resource_Translater.Util.Config;

namespace Ellab_Resource_Translater
{
    public partial class Settings : Form
    {
        private int setup = 0;
        public Settings()
        {
            InitializeComponent();
            this.FormClosed += Settings_Exit;
        }

        private void EMsuiteBrowse_Click(object sender, EventArgs e)
        {
            /*var folderPaths = EMsuitePath.Text.Reverse<char>().ToString().Split('\\', 2);
            if(folderPaths.Length > 1)
            {
                folderBrowserDialogEMsuite.InitialDirectory = folderPaths[1].Reverse<char>().ToString();
            }*/
            var dialogResult = EMsuiteFBDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                EMsuitePath.Text = EMsuiteFBDialog.SelectedPath;
            }
        }

        private void NotEmBrowse_Click(object sender, EventArgs e)
        {
            var dialogResult = ValFBDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ValPath.Text = ValFBDialog.SelectedPath;
            }
        }

        private void TranslationCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // While Loading I don't want this to run
            if (setup > 0)
                return;

            var config = Config.Get();
            var languagePairs = Config.DefaultLanguages();

            FormUtils.SaveCheckBoxListChangeLocalised(
                list: config.languagesToAiTranslate,
                checkedListBox: TranslationCheckedListBox,
                localiser: languagePairs);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            var config = Config.Get();
            var languagePairs = Config.DefaultLanguages();

            setup++;
            FormUtils.LoadCheckboxListLocalised(
                list: config.languagesToAiTranslate,
                checkedListBox: TranslationCheckedListBox,
                localiser: languagePairs
                );

            FormUtils.LinkVariable(ref DelayNumeric, config.checkDelay);
            FormUtils.LinkVariable(ref EMsuitePath, config.EMPath);
            FormUtils.LinkVariable(ref ValPath, config.ValPath);
            FormUtils.LinkVariable(ref ReaderNumeric, config.threadsToUse);
            FormUtils.LinkVariable(ref InserterNumeric, config.insertersToUse);
            FormUtils.LinkVariable(ref CloseOnSuccess, config.closeOnceDone);

            Config.AssignSizeSetting(this, (s) => config.SettingWindowSize = s, this.Size);
            setup--;

            TooltipNormal.SetToolTip(ReaderLabel, "Amount of threads to use when reading/writing from/to the disk");
            TooltipNormal.SetToolTip(InserterLabel, "Amount of threads to use when writing to the database");
            TooltipNormal.SetToolTip(DelayLabel, "Amount of ms between each wait check when waiting.");
        }

        private void Settings_Exit(object? sender, EventArgs e)
        {
            Config.Save();
        }
    }
}
