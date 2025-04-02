namespace Ellab_Resource_Translater
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SettingsButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            translationPanel = new Panel();
            translationLabel = new Label();
            translationCheckedListBox = new CheckedListBox();
            progresPanel = new Panel();
            CancellationButton = new Button();
            progressListView = new ListView();
            progressTracker = new Label();
            progressTitle = new Label();
            splitContainer1 = new SplitContainer();
            DBConnectionPanel = new Panel();
            DBConnectionSetup = new Button();
            AzureSettingsSetup = new Button();
            label2 = new Label();
            RefreshAzureButton = new Button();
            AzureConnectionStatus = new Label();
            RefreshConnectionButton = new Button();
            connectionStatus = new Label();
            ButtonPanel = new Panel();
            EMandValButton = new Button();
            EMSuiteButton = new Button();
            ValSuiteButton = new Button();
            TooltipNormal = new ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            translationPanel.SuspendLayout();
            progresPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            DBConnectionPanel.SuspendLayout();
            ButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // SettingsButton
            // 
            SettingsButton.Dock = DockStyle.Right;
            SettingsButton.Location = new Point(234, 5);
            SettingsButton.MinimumSize = new Size(0, 30);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(75, 30);
            SettingsButton.TabIndex = 4;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(translationPanel);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 40);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(15);
            flowLayoutPanel1.Size = new Size(314, 571);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // translationPanel
            // 
            translationPanel.AutoSize = true;
            translationPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            translationPanel.Controls.Add(translationLabel);
            translationPanel.Controls.Add(translationCheckedListBox);
            translationPanel.Location = new Point(18, 18);
            translationPanel.MinimumSize = new Size(100, 0);
            translationPanel.Name = "translationPanel";
            translationPanel.Size = new Size(100, 217);
            translationPanel.TabIndex = 5;
            // 
            // translationLabel
            // 
            translationLabel.Dock = DockStyle.Top;
            translationLabel.Location = new Point(0, 0);
            translationLabel.Name = "translationLabel";
            translationLabel.Size = new Size(100, 15);
            translationLabel.TabIndex = 5;
            translationLabel.Text = "Translation";
            translationLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // translationCheckedListBox
            // 
            translationCheckedListBox.CheckOnClick = true;
            translationCheckedListBox.FormattingEnabled = true;
            translationCheckedListBox.Items.AddRange(new object[] { "DE", "ES", "FR", "IT", "JA", "KO", "NL", "PL", "PT", "TR", "ZH" });
            translationCheckedListBox.Location = new Point(0, 15);
            translationCheckedListBox.Margin = new Padding(0);
            translationCheckedListBox.MinimumSize = new Size(100, 40);
            translationCheckedListBox.Name = "translationCheckedListBox";
            translationCheckedListBox.Size = new Size(100, 202);
            translationCheckedListBox.Sorted = true;
            translationCheckedListBox.TabIndex = 5;
            translationCheckedListBox.ItemCheck += TranslationCheckedListBox_CheckChanged;
            // 
            // progresPanel
            // 
            progresPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            progresPanel.BackColor = Color.Transparent;
            progresPanel.Controls.Add(CancellationButton);
            progresPanel.Controls.Add(progressListView);
            progresPanel.Controls.Add(progressTracker);
            progresPanel.Controls.Add(progressTitle);
            progresPanel.Dock = DockStyle.Fill;
            progresPanel.Location = new Point(0, 0);
            progresPanel.Margin = new Padding(0);
            progresPanel.MinimumSize = new Size(100, 100);
            progresPanel.Name = "progresPanel";
            progresPanel.Padding = new Padding(15);
            progresPanel.Size = new Size(565, 611);
            progresPanel.TabIndex = 0;
            // 
            // CancellationButton
            // 
            CancellationButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CancellationButton.Enabled = false;
            CancellationButton.Location = new Point(474, 7);
            CancellationButton.Name = "CancellationButton";
            CancellationButton.Size = new Size(75, 30);
            CancellationButton.TabIndex = 1;
            CancellationButton.Text = "Cancel";
            CancellationButton.UseVisualStyleBackColor = true;
            CancellationButton.Click += CancellationButton_Click;
            // 
            // progressListView
            // 
            progressListView.AccessibleRole = AccessibleRole.None;
            progressListView.BackColor = SystemColors.Window;
            progressListView.Dock = DockStyle.Fill;
            progressListView.HeaderStyle = ColumnHeaderStyle.None;
            progressListView.Location = new Point(15, 45);
            progressListView.MultiSelect = false;
            progressListView.Name = "progressListView";
            progressListView.ShowGroups = false;
            progressListView.Size = new Size(535, 551);
            progressListView.TabIndex = 0;
            progressListView.TabStop = false;
            progressListView.UseCompatibleStateImageBehavior = false;
            progressListView.View = View.List;
            // 
            // progressTracker
            // 
            progressTracker.Dock = DockStyle.Top;
            progressTracker.Location = new Point(15, 30);
            progressTracker.Margin = new Padding(0);
            progressTracker.Name = "progressTracker";
            progressTracker.Size = new Size(535, 15);
            progressTracker.TabIndex = 0;
            progressTracker.Text = "x out of y";
            progressTracker.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressTitle
            // 
            progressTitle.Dock = DockStyle.Top;
            progressTitle.Location = new Point(15, 15);
            progressTitle.Margin = new Padding(0);
            progressTitle.Name = "progressTitle";
            progressTitle.Size = new Size(535, 15);
            progressTitle.TabIndex = 0;
            progressTitle.Text = "nothing running";
            progressTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.Transparent;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.Transparent;
            splitContainer1.Panel1.Controls.Add(progresPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.Transparent;
            splitContainer1.Panel2.Controls.Add(DBConnectionPanel);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer1.Panel2.Controls.Add(ButtonPanel);
            splitContainer1.Panel2MinSize = 311;
            splitContainer1.Size = new Size(884, 611);
            splitContainer1.SplitterDistance = 565;
            splitContainer1.SplitterIncrement = 5;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            splitContainer1.TabStop = false;
            // 
            // DBConnectionPanel
            // 
            DBConnectionPanel.BackColor = Color.Transparent;
            DBConnectionPanel.Controls.Add(DBConnectionSetup);
            DBConnectionPanel.Controls.Add(AzureSettingsSetup);
            DBConnectionPanel.Controls.Add(label2);
            DBConnectionPanel.Controls.Add(RefreshAzureButton);
            DBConnectionPanel.Controls.Add(AzureConnectionStatus);
            DBConnectionPanel.Controls.Add(RefreshConnectionButton);
            DBConnectionPanel.Controls.Add(connectionStatus);
            DBConnectionPanel.Dock = DockStyle.Bottom;
            DBConnectionPanel.ForeColor = SystemColors.ControlText;
            DBConnectionPanel.Location = new Point(0, 520);
            DBConnectionPanel.Name = "DBConnectionPanel";
            DBConnectionPanel.Size = new Size(314, 91);
            DBConnectionPanel.TabIndex = 6;
            // 
            // DBConnectionSetup
            // 
            DBConnectionSetup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DBConnectionSetup.Location = new Point(244, 63);
            DBConnectionSetup.Name = "DBConnectionSetup";
            DBConnectionSetup.Size = new Size(65, 23);
            DBConnectionSetup.TabIndex = 2;
            DBConnectionSetup.Text = "Database";
            DBConnectionSetup.UseVisualStyleBackColor = true;
            DBConnectionSetup.Click += DBConnectionSetup_Click;
            // 
            // AzureSettingsSetup
            // 
            AzureSettingsSetup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AzureSettingsSetup.Location = new Point(178, 63);
            AzureSettingsSetup.Name = "AzureSettingsSetup";
            AzureSettingsSetup.Size = new Size(60, 23);
            AzureSettingsSetup.TabIndex = 4;
            AzureSettingsSetup.Text = "Azure login";
            AzureSettingsSetup.UseVisualStyleBackColor = true;
            AzureSettingsSetup.Click += AzureSettingsSetup_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(114, 67);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 6;
            label2.Text = "Setups:";
            // 
            // RefreshAzureButton
            // 
            RefreshAzureButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshAzureButton.Location = new Point(249, 11);
            RefreshAzureButton.Name = "RefreshAzureButton";
            RefreshAzureButton.Size = new Size(60, 23);
            RefreshAzureButton.TabIndex = 5;
            RefreshAzureButton.Text = "Refresh";
            RefreshAzureButton.UseVisualStyleBackColor = true;
            RefreshAzureButton.Click += RefreshAzureButton_Click;
            // 
            // AzureConnectionStatus
            // 
            AzureConnectionStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AzureConnectionStatus.Location = new Point(15, 15);
            AzureConnectionStatus.Name = "AzureConnectionStatus";
            AzureConnectionStatus.Size = new Size(218, 19);
            AzureConnectionStatus.TabIndex = 3;
            AzureConnectionStatus.Text = "Azure AI Connection Status";
            AzureConnectionStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RefreshConnectionButton
            // 
            RefreshConnectionButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshConnectionButton.Location = new Point(249, 34);
            RefreshConnectionButton.Name = "RefreshConnectionButton";
            RefreshConnectionButton.Size = new Size(60, 23);
            RefreshConnectionButton.TabIndex = 1;
            RefreshConnectionButton.Text = "Refresh";
            RefreshConnectionButton.UseVisualStyleBackColor = true;
            RefreshConnectionButton.Click += RefreshConnectionButton_Click;
            // 
            // connectionStatus
            // 
            connectionStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            connectionStatus.Location = new Point(15, 34);
            connectionStatus.Name = "connectionStatus";
            connectionStatus.Size = new Size(219, 19);
            connectionStatus.TabIndex = 0;
            connectionStatus.Text = "Database Connection Status";
            connectionStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ButtonPanel
            // 
            ButtonPanel.AutoSize = true;
            ButtonPanel.Controls.Add(EMandValButton);
            ButtonPanel.Controls.Add(EMSuiteButton);
            ButtonPanel.Controls.Add(ValSuiteButton);
            ButtonPanel.Controls.Add(SettingsButton);
            ButtonPanel.Dock = DockStyle.Top;
            ButtonPanel.Location = new Point(0, 0);
            ButtonPanel.Margin = new Padding(0);
            ButtonPanel.MinimumSize = new Size(0, 40);
            ButtonPanel.Name = "ButtonPanel";
            ButtonPanel.Padding = new Padding(5);
            ButtonPanel.Size = new Size(314, 40);
            ButtonPanel.TabIndex = 0;
            // 
            // EMandValButton
            // 
            EMandValButton.BackColor = SystemColors.Control;
            EMandValButton.Dock = DockStyle.Right;
            EMandValButton.Location = new Point(9, 5);
            EMandValButton.Margin = new Padding(0);
            EMandValButton.MinimumSize = new Size(0, 30);
            EMandValButton.Name = "EMandValButton";
            EMandValButton.Size = new Size(75, 30);
            EMandValButton.TabIndex = 1;
            EMandValButton.Text = "EM && Val";
            EMandValButton.UseVisualStyleBackColor = true;
            EMandValButton.Click += EMandValButton_Click;
            // 
            // EMSuiteButton
            // 
            EMSuiteButton.BackColor = SystemColors.Control;
            EMSuiteButton.Dock = DockStyle.Right;
            EMSuiteButton.Location = new Point(84, 5);
            EMSuiteButton.Margin = new Padding(0);
            EMSuiteButton.MinimumSize = new Size(0, 30);
            EMSuiteButton.Name = "EMSuiteButton";
            EMSuiteButton.Size = new Size(75, 30);
            EMSuiteButton.TabIndex = 2;
            EMSuiteButton.Text = "EMSuite";
            EMSuiteButton.UseVisualStyleBackColor = true;
            EMSuiteButton.Click += EMSuite_Initiation;
            // 
            // ValSuiteButton
            // 
            ValSuiteButton.BackColor = SystemColors.Control;
            ValSuiteButton.Dock = DockStyle.Right;
            ValSuiteButton.Location = new Point(159, 5);
            ValSuiteButton.MinimumSize = new Size(0, 30);
            ValSuiteButton.Name = "ValSuiteButton";
            ValSuiteButton.Size = new Size(75, 30);
            ValSuiteButton.TabIndex = 3;
            ValSuiteButton.Text = "ValSuite";
            ValSuiteButton.UseVisualStyleBackColor = true;
            ValSuiteButton.Click += ValSuite_Initiation;
            // 
            // TooltipNormal
            // 
            TooltipNormal.AutomaticDelay = 100;
            TooltipNormal.AutoPopDelay = 0;
            TooltipNormal.InitialDelay = 100;
            TooltipNormal.ReshowDelay = 20;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SkyBlue;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(884, 611);
            Controls.Add(splitContainer1);
            DoubleBuffered = true;
            ForeColor = Color.Black;
            Name = "MainForm";
            Text = "Ellab Resource Tranlator";
            FormClosed += MainForm_Closed;
            Load += Form1_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            translationPanel.ResumeLayout(false);
            progresPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            DBConnectionPanel.ResumeLayout(false);
            DBConnectionPanel.PerformLayout();
            ButtonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button SettingsButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel translationPanel;
        private Label translationLabel;
        private CheckedListBox translationCheckedListBox;
        private Panel progresPanel;
        private ListView progressListView;
        private Label progressTracker;
        private SplitContainer splitContainer1;
        private Panel ButtonPanel;
        private Button EMSuiteButton;
        private Button ValSuiteButton;
        private Button EMandValButton;
        private Label progressTitle;
        private Panel DBConnectionPanel;
        private Label connectionStatus;
        private Button DBConnectionSetup;
        private Button RefreshConnectionButton;
        private Button AzureSettingsSetup;
        private Label AzureConnectionStatus;
        private Label label2;
        private Button RefreshAzureButton;
        private Button CancellationButton;
        private ToolTip TooltipNormal;
    }
}
