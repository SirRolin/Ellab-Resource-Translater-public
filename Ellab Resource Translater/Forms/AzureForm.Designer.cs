namespace Ellab_Resource_Translater.Forms
{
    partial class AzureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SaveButton = new Button();
            HeaderPanel = new Panel();
            button1 = new Button();
            AzurePanel = new TableLayoutPanel();
            AzureTitleLabel = new Label();
            AzureRegionLabel = new Label();
            AzureKeyLabel = new Label();
            AzureKey = new TextBox();
            AzureURILabel = new Label();
            AzureURI = new TextBox();
            AzureRegion = new ComboBox();
            toolTip1 = new ToolTip(components);
            HeaderPanel.SuspendLayout();
            AzurePanel.SuspendLayout();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveButton.AutoSize = true;
            SaveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SaveButton.Location = new Point(818, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(90, 25);
            SaveButton.TabIndex = 3;
            SaveButton.Text = "Save Changes";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // HeaderPanel
            // 
            HeaderPanel.AutoSize = true;
            HeaderPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            HeaderPanel.Controls.Add(button1);
            HeaderPanel.Controls.Add(SaveButton);
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.Name = "HeaderPanel";
            HeaderPanel.Size = new Size(911, 31);
            HeaderPanel.TabIndex = 7;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(1180, 3);
            button1.Name = "button1";
            button1.Size = new Size(90, 25);
            button1.TabIndex = 2;
            button1.Text = "Save Changes";
            button1.UseVisualStyleBackColor = true;
            // 
            // AzurePanel
            // 
            AzurePanel.AutoSize = true;
            AzurePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AzurePanel.ColumnCount = 2;
            AzurePanel.ColumnStyles.Add(new ColumnStyle());
            AzurePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AzurePanel.Controls.Add(AzureTitleLabel, 0, 0);
            AzurePanel.Controls.Add(AzureRegionLabel, 0, 3);
            AzurePanel.Controls.Add(AzureKeyLabel, 0, 1);
            AzurePanel.Controls.Add(AzureKey, 1, 1);
            AzurePanel.Controls.Add(AzureURILabel, 0, 2);
            AzurePanel.Controls.Add(AzureURI, 1, 2);
            AzurePanel.Controls.Add(AzureRegion, 1, 3);
            AzurePanel.Dock = DockStyle.Top;
            AzurePanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            AzurePanel.Location = new Point(0, 31);
            AzurePanel.Margin = new Padding(15);
            AzurePanel.Name = "AzurePanel";
            AzurePanel.Padding = new Padding(15);
            AzurePanel.RowCount = 4;
            AzurePanel.RowStyles.Add(new RowStyle());
            AzurePanel.RowStyles.Add(new RowStyle());
            AzurePanel.RowStyles.Add(new RowStyle());
            AzurePanel.RowStyles.Add(new RowStyle());
            AzurePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            AzurePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            AzurePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            AzurePanel.Size = new Size(911, 138);
            AzurePanel.TabIndex = 8;
            // 
            // AzureTitleLabel
            // 
            AzurePanel.SetColumnSpan(AzureTitleLabel, 2);
            AzureTitleLabel.Dock = DockStyle.Fill;
            AzureTitleLabel.Location = new Point(18, 18);
            AzureTitleLabel.Margin = new Padding(3);
            AzureTitleLabel.Name = "AzureTitleLabel";
            AzureTitleLabel.Size = new Size(875, 15);
            AzureTitleLabel.TabIndex = 3;
            AzureTitleLabel.Text = "Azure Translation API";
            AzureTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AzureRegionLabel
            // 
            AzureRegionLabel.AutoSize = true;
            AzureRegionLabel.Dock = DockStyle.Fill;
            AzureRegionLabel.Location = new Point(18, 94);
            AzureRegionLabel.Name = "AzureRegionLabel";
            AzureRegionLabel.Size = new Size(44, 29);
            AzureRegionLabel.TabIndex = 9;
            AzureRegionLabel.Text = "Region";
            AzureRegionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AzureKeyLabel
            // 
            AzureKeyLabel.AutoSize = true;
            AzureKeyLabel.Dock = DockStyle.Fill;
            AzureKeyLabel.Location = new Point(18, 36);
            AzureKeyLabel.Name = "AzureKeyLabel";
            AzureKeyLabel.Size = new Size(44, 29);
            AzureKeyLabel.TabIndex = 0;
            AzureKeyLabel.Text = "Key";
            AzureKeyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AzureKey
            // 
            AzureKey.Dock = DockStyle.Fill;
            AzureKey.Location = new Point(68, 39);
            AzureKey.Name = "AzureKey";
            AzureKey.Size = new Size(825, 23);
            AzureKey.TabIndex = 1;
            toolTip1.SetToolTip(AzureKey, "Usually found on https://dev.azure.com/ once you create a project (or use existing)");
            // 
            // AzureURILabel
            // 
            AzureURILabel.AutoSize = true;
            AzureURILabel.Dock = DockStyle.Fill;
            AzureURILabel.Location = new Point(18, 65);
            AzureURILabel.Name = "AzureURILabel";
            AzureURILabel.Size = new Size(44, 29);
            AzureURILabel.TabIndex = 1;
            AzureURILabel.Text = "URI";
            AzureURILabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AzureURI
            // 
            AzureURI.Dock = DockStyle.Fill;
            AzureURI.Location = new Point(68, 68);
            AzureURI.Name = "AzureURI";
            AzureURI.Size = new Size(825, 23);
            AzureURI.TabIndex = 2;
            AzureURI.Text = "https://api.cognitive.microsofttranslator.com/";
            toolTip1.SetToolTip(AzureURI, "https://api.cognitive.microsofttranslator.com/");
            // 
            // AzureRegion
            // 
            AzureRegion.AutoCompleteSource = AutoCompleteSource.ListItems;
            AzureRegion.Dock = DockStyle.Fill;
            AzureRegion.FormattingEnabled = true;
            AzureRegion.Items.AddRange(new object[] { "asia", "asiapacific", "australia", "australiacentral", "australiacentral2", "australiaeast", "australiasoutheast", "brazil", "brazilsouth", "brazilsoutheast", "brazilus", "canada", "canadacentral", "canadaeast", "centralindia", "centralus", "centraluseuap", "centralusstage", "eastasia", "eastasiastage", "eastus", "eastus2", "eastus2euap", "eastus2stage", "eastusstage", "eastusstg", "europe", "france", "francecentral", "francesouth", "germany", "germanynorth", "germanywestcentral", "global", "india", "japan", "japaneast", "japanwest", "jioindiacentral", "jioindiawest", "korea", "koreacentral", "koreasouth", "northcentralus", "northcentralusstage", "northeurope", "norway", "norwayeast", "norwaywest", "polandcentral", "qatarcentral", "singapore", "southafrica", "southafricanorth", "southafricawest", "southcentralus", "southcentralusstage", "southcentralusstg", "southeastasia", "southeastasiastage", "southindia", "swedencentral", "switzerland", "switzerlandnorth", "switzerlandwest", "uae", "uaecentral", "uaenorth", "uk", "uksouth", "ukwest", "unitedstates", "unitedstateseuap", "westcentralus", "westeurope", "westindia", "westus", "westus2", "westus2stage", "westus3", "westusstage" });
            AzureRegion.Location = new Point(68, 97);
            AzureRegion.Name = "AzureRegion";
            AzureRegion.Size = new Size(825, 23);
            AzureRegion.TabIndex = 10;
            AzureRegion.Text = "northeurope";
            // 
            // AzureForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(911, 450);
            Controls.Add(AzurePanel);
            Controls.Add(HeaderPanel);
            Name = "AzureForm";
            Text = "Azure";
            HeaderPanel.ResumeLayout(false);
            HeaderPanel.PerformLayout();
            AzurePanel.ResumeLayout(false);
            AzurePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveButton;
        private Panel HeaderPanel;
        private Button button1;
        private TableLayoutPanel AzurePanel;
        private Label AzureRegionLabel;
        private Label AzureTitleLabel;
        private Label AzureKeyLabel;
        private TextBox AzureKey;
        private Label AzureURILabel;
        private TextBox AzureURI;
        private ComboBox AzureRegion;
        private ToolTip toolTip1;
    }
}