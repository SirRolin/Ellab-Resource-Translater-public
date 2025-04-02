using Ellab_Resource_Translater.Util;
using System.Windows.Forms;

namespace Ellab_Resource_Translater
{
    partial class Settings
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
            EMsuiteFBDialog = new FolderBrowserDialog();
            ValFBDialog = new FolderBrowserDialog();
            LocationTablePanel = new TableLayoutPanel();
            EMSuiteText = new Label();
            EMsuitePath = new TextBox();
            EMsuiteFBButton = new Button();
            ValText = new Label();
            ValPath = new TextBox();
            ValFBButton = new Button();
            TranslationPanel = new Panel();
            TranslationLabel = new Label();
            TranslationCheckedListBox = new CheckedListBox();
            FlowLayoutPanel1 = new FlowLayoutPanel();
            ReaderPanel = new Panel();
            ReaderNumeric = new NumericUpDown();
            ReaderLabel = new Label();
            InserterPanel = new Panel();
            InserterNumeric = new NumericUpDown();
            InserterLabel = new Label();
            DelayPanel = new Panel();
            DelayNumeric = new NumericUpDown();
            DelayLabel = new Label();
            CloseOnSuccess = new CheckBox();
            TooltipNormal = new ToolTip(components);
            LocationTablePanel.SuspendLayout();
            TranslationPanel.SuspendLayout();
            FlowLayoutPanel1.SuspendLayout();
            ReaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReaderNumeric).BeginInit();
            InserterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InserterNumeric).BeginInit();
            DelayPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DelayNumeric).BeginInit();
            SuspendLayout();
            // 
            // ValFBDialog
            // 
            ValFBDialog.AddToRecent = false;
            // 
            // LocationTablePanel
            // 
            LocationTablePanel.AutoSize = true;
            LocationTablePanel.ColumnCount = 3;
            LocationTablePanel.ColumnStyles.Add(new ColumnStyle());
            LocationTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            LocationTablePanel.ColumnStyles.Add(new ColumnStyle());
            LocationTablePanel.Controls.Add(EMSuiteText, 0, 0);
            LocationTablePanel.Controls.Add(EMsuitePath, 1, 0);
            LocationTablePanel.Controls.Add(EMsuiteFBButton, 2, 0);
            LocationTablePanel.Controls.Add(ValText, 0, 1);
            LocationTablePanel.Controls.Add(ValPath, 1, 1);
            LocationTablePanel.Controls.Add(ValFBButton, 2, 1);
            LocationTablePanel.Dock = DockStyle.Top;
            LocationTablePanel.Location = new Point(0, 0);
            LocationTablePanel.Margin = new Padding(15);
            LocationTablePanel.Name = "LocationTablePanel";
            LocationTablePanel.Padding = new Padding(15);
            LocationTablePanel.RowCount = 2;
            LocationTablePanel.RowStyles.Add(new RowStyle());
            LocationTablePanel.RowStyles.Add(new RowStyle());
            LocationTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            LocationTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            LocationTablePanel.Size = new Size(584, 88);
            LocationTablePanel.TabIndex = 4;
            // 
            // EMSuiteText
            // 
            EMSuiteText.AutoSize = true;
            EMSuiteText.Dock = DockStyle.Fill;
            EMSuiteText.Location = new Point(18, 15);
            EMSuiteText.Name = "EMSuiteText";
            EMSuiteText.Size = new Size(96, 29);
            EMSuiteText.TabIndex = 0;
            EMSuiteText.Text = "EMSuite location";
            EMSuiteText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EMsuitePath
            // 
            EMsuitePath.Dock = DockStyle.Fill;
            EMsuitePath.Location = new Point(120, 18);
            EMsuitePath.Name = "EMsuitePath";
            EMsuitePath.Size = new Size(375, 23);
            EMsuitePath.TabIndex = 0;
            // 
            // EMsuiteFBButton
            // 
            EMsuiteFBButton.Dock = DockStyle.Right;
            EMsuiteFBButton.Location = new Point(501, 18);
            EMsuiteFBButton.Name = "EMsuiteFBButton";
            EMsuiteFBButton.Size = new Size(65, 23);
            EMsuiteFBButton.TabIndex = 2;
            EMsuiteFBButton.Text = "...";
            EMsuiteFBButton.UseVisualStyleBackColor = true;
            EMsuiteFBButton.Click += EMsuiteBrowse_Click;
            // 
            // ValText
            // 
            ValText.AutoSize = true;
            ValText.Dock = DockStyle.Fill;
            ValText.Location = new Point(18, 44);
            ValText.Name = "ValText";
            ValText.Size = new Size(96, 29);
            ValText.TabIndex = 1;
            ValText.Text = "ValSuite location";
            ValText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ValPath
            // 
            ValPath.Dock = DockStyle.Fill;
            ValPath.Location = new Point(120, 47);
            ValPath.Name = "ValPath";
            ValPath.Size = new Size(375, 23);
            ValPath.TabIndex = 3;
            // 
            // ValFBButton
            // 
            ValFBButton.Dock = DockStyle.Right;
            ValFBButton.Location = new Point(501, 47);
            ValFBButton.Name = "ValFBButton";
            ValFBButton.Size = new Size(65, 23);
            ValFBButton.TabIndex = 4;
            ValFBButton.Text = "...";
            ValFBButton.UseVisualStyleBackColor = true;
            ValFBButton.Click += NotEmBrowse_Click;
            // 
            // TranslationPanel
            // 
            TranslationPanel.AutoSize = true;
            TranslationPanel.Controls.Add(TranslationLabel);
            TranslationPanel.Controls.Add(TranslationCheckedListBox);
            TranslationPanel.Location = new Point(15, 15);
            TranslationPanel.Margin = new Padding(15, 0, 15, 0);
            TranslationPanel.MinimumSize = new Size(100, 0);
            TranslationPanel.Name = "TranslationPanel";
            TranslationPanel.Size = new Size(103, 223);
            TranslationPanel.TabIndex = 0;
            // 
            // TranslationLabel
            // 
            TranslationLabel.Dock = DockStyle.Top;
            TranslationLabel.Location = new Point(0, 0);
            TranslationLabel.Margin = new Padding(15, 15, 15, 0);
            TranslationLabel.Name = "TranslationLabel";
            TranslationLabel.Size = new Size(103, 15);
            TranslationLabel.TabIndex = 3;
            TranslationLabel.Text = "Ai Translation";
            TranslationLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TranslationCheckedListBox
            // 
            TranslationCheckedListBox.CheckOnClick = true;
            TranslationCheckedListBox.Cursor = Cursors.Hand;
            TranslationCheckedListBox.FormattingEnabled = true;
            TranslationCheckedListBox.Items.AddRange(new object[] { "DE", "ES", "FR", "IT", "JA", "KO", "NL", "PL", "PT", "TR", "ZH" });
            TranslationCheckedListBox.Location = new Point(0, 18);
            TranslationCheckedListBox.MultiColumn = true;
            TranslationCheckedListBox.Name = "TranslationCheckedListBox";
            TranslationCheckedListBox.RightToLeft = RightToLeft.No;
            TranslationCheckedListBox.Size = new Size(100, 202);
            TranslationCheckedListBox.Sorted = true;
            TranslationCheckedListBox.TabIndex = 5;
            TranslationCheckedListBox.ItemCheck += TranslationCheckedListBox_SelectedIndexChanged;
            // 
            // FlowLayoutPanel1
            // 
            FlowLayoutPanel1.Controls.Add(TranslationPanel);
            FlowLayoutPanel1.Controls.Add(ReaderPanel);
            FlowLayoutPanel1.Controls.Add(InserterPanel);
            FlowLayoutPanel1.Controls.Add(DelayPanel);
            FlowLayoutPanel1.Controls.Add(CloseOnSuccess);
            FlowLayoutPanel1.Dock = DockStyle.Fill;
            FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            FlowLayoutPanel1.Location = new Point(0, 88);
            FlowLayoutPanel1.Margin = new Padding(0);
            FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            FlowLayoutPanel1.Padding = new Padding(0, 15, 0, 15);
            FlowLayoutPanel1.Size = new Size(584, 273);
            FlowLayoutPanel1.TabIndex = 5;
            // 
            // ReaderPanel
            // 
            ReaderPanel.AutoSize = true;
            ReaderPanel.Controls.Add(ReaderNumeric);
            ReaderPanel.Controls.Add(ReaderLabel);
            ReaderPanel.Location = new Point(148, 15);
            ReaderPanel.Margin = new Padding(15, 0, 15, 0);
            ReaderPanel.Name = "ReaderPanel";
            ReaderPanel.Size = new Size(178, 52);
            ReaderPanel.TabIndex = 1;
            // 
            // ReaderNumeric
            // 
            ReaderNumeric.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReaderNumeric.Location = new Point(3, 26);
            ReaderNumeric.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            ReaderNumeric.Name = "ReaderNumeric";
            ReaderNumeric.Size = new Size(172, 23);
            ReaderNumeric.TabIndex = 1;
            ReaderNumeric.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // ReaderLabel
            // 
            ReaderLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReaderLabel.AutoSize = true;
            ReaderLabel.Location = new Point(3, 3);
            ReaderLabel.Margin = new Padding(3);
            ReaderLabel.Name = "ReaderLabel";
            ReaderLabel.Padding = new Padding(3);
            ReaderLabel.Size = new Size(170, 21);
            ReaderLabel.TabIndex = 0;
            ReaderLabel.Text = "Readers (0 = sync, norm 8-32)";
            // 
            // InserterPanel
            // 
            InserterPanel.Controls.Add(InserterNumeric);
            InserterPanel.Controls.Add(InserterLabel);
            InserterPanel.Location = new Point(148, 67);
            InserterPanel.Margin = new Padding(15, 0, 15, 0);
            InserterPanel.Name = "InserterPanel";
            InserterPanel.Size = new Size(178, 52);
            InserterPanel.TabIndex = 3;
            // 
            // InserterNumeric
            // 
            InserterNumeric.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InserterNumeric.Location = new Point(3, 26);
            InserterNumeric.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            InserterNumeric.Name = "InserterNumeric";
            InserterNumeric.Size = new Size(172, 23);
            InserterNumeric.TabIndex = 1;
            InserterNumeric.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // InserterLabel
            // 
            InserterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InserterLabel.AutoSize = true;
            InserterLabel.Location = new Point(3, 3);
            InserterLabel.Margin = new Padding(3);
            InserterLabel.Name = "InserterLabel";
            InserterLabel.Padding = new Padding(3);
            InserterLabel.Size = new Size(135, 21);
            InserterLabel.TabIndex = 0;
            InserterLabel.Text = "Inserters (0 = sync, 1-8)";
            // 
            // DelayPanel
            // 
            DelayPanel.AutoSize = true;
            DelayPanel.Controls.Add(DelayNumeric);
            DelayPanel.Controls.Add(DelayLabel);
            DelayPanel.Location = new Point(148, 119);
            DelayPanel.Margin = new Padding(15, 0, 15, 0);
            DelayPanel.Name = "DelayPanel";
            DelayPanel.Size = new Size(178, 52);
            DelayPanel.TabIndex = 4;
            // 
            // DelayNumeric
            // 
            DelayNumeric.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DelayNumeric.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            DelayNumeric.Location = new Point(3, 26);
            DelayNumeric.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            DelayNumeric.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            DelayNumeric.Name = "DelayNumeric";
            DelayNumeric.Size = new Size(172, 23);
            DelayNumeric.TabIndex = 1;
            DelayNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // DelayLabel
            // 
            DelayLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new Point(3, 3);
            DelayLabel.Margin = new Padding(3);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Padding = new Padding(3);
            DelayLabel.Size = new Size(155, 21);
            DelayLabel.TabIndex = 0;
            DelayLabel.Text = "ms to wait between checks";
            // 
            // CloseOnSuccess
            // 
            CloseOnSuccess.AutoSize = true;
            CloseOnSuccess.Location = new Point(151, 171);
            CloseOnSuccess.Margin = new Padding(18, 0, 15, 0);
            CloseOnSuccess.Name = "CloseOnSuccess";
            CloseOnSuccess.Size = new Size(172, 19);
            CloseOnSuccess.TabIndex = 2;
            CloseOnSuccess.Text = "Close Program if Successful";
            CloseOnSuccess.UseVisualStyleBackColor = true;
            // 
            // TooltipNormal
            // 
            TooltipNormal.AutomaticDelay = 100;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(FlowLayoutPanel1);
            Controls.Add(LocationTablePanel);
            Name = "Settings";
            Text = "Settings";
            Load += Settings_Load;
            LocationTablePanel.ResumeLayout(false);
            LocationTablePanel.PerformLayout();
            TranslationPanel.ResumeLayout(false);
            FlowLayoutPanel1.ResumeLayout(false);
            FlowLayoutPanel1.PerformLayout();
            ReaderPanel.ResumeLayout(false);
            ReaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReaderNumeric).EndInit();
            InserterPanel.ResumeLayout(false);
            InserterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InserterNumeric).EndInit();
            DelayPanel.ResumeLayout(false);
            DelayPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DelayNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel LocationTablePanel;
        private Label EMSuiteText;
        private TextBox EMsuitePath;
        private Button EMsuiteFBButton;
        private Label ValText;
        private TextBox ValPath;
        private Button ValFBButton;
        private FolderBrowserDialog EMsuiteFBDialog;
        private FolderBrowserDialog ValFBDialog;
        private Panel TranslationPanel;
        private Label TranslationLabel;
        private CheckedListBox TranslationCheckedListBox;
        private FlowLayoutPanel FlowLayoutPanel1;
        private Panel ReaderPanel;
        private NumericUpDown ReaderNumeric;
        private Label ReaderLabel;
        private CheckBox CloseOnSuccess;
        private Panel InserterPanel;
        private NumericUpDown InserterNumeric;
        private Label InserterLabel;
        private ToolTip TooltipNormal;
        private Panel DelayPanel;
        private NumericUpDown DelayNumeric;
        private Label DelayLabel;
    }
}