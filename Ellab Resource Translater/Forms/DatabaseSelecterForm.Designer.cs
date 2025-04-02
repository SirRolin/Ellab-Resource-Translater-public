namespace Ellab_Resource_Translater.Forms
{
    partial class DatabaseSelecterForm
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
            MySQLPanel = new TableLayoutPanel();
            MySqlPortText = new TextBox();
            MySqlPortLabel = new Label();
            MySqlTitleLabel = new Label();
            MySqlLabel1 = new Label();
            MySqlServerText = new TextBox();
            MySqlLabel2 = new Label();
            MySqlDatabaseText = new TextBox();
            MySqlLabel3 = new Label();
            MySqlUserIDText = new TextBox();
            MySqlLabel4 = new Label();
            MySqlPasswordText = new TextBox();
            MySqlISLabel = new Label();
            MySqlISCheckBox = new CheckBox();
            HeaderPanel = new Panel();
            ResetToHardcodedButton = new Button();
            SaveButton = new Button();
            connectionStringChoice = new ComboBox();
            ServerTypeLabel = new Label();
            SqlServerPanel = new TableLayoutPanel();
            MSSqlPortText = new TextBox();
            MSSqlPortLabel = new Label();
            MSSqlTitleLabel = new Label();
            MSSqlServerLabel = new Label();
            MSSqlServerText = new TextBox();
            MSSqlDatabaseLabel = new Label();
            MSSqlDatabaseText = new TextBox();
            MSSqlISLabel = new Label();
            MSSqlISCheckBox = new CheckBox();
            MSSqlUserIDLabel = new Label();
            MSSqlUserIDText = new TextBox();
            MSSqlPasswordLabel = new Label();
            MSSqlPasswordText = new TextBox();
            PostgreSqlPanel = new TableLayoutPanel();
            PostgresPasswordText = new TextBox();
            label6 = new Label();
            PostgresUserIDText = new TextBox();
            label5 = new Label();
            PostgresPortText = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            PostgresHostText = new TextBox();
            label4 = new Label();
            PostgresDatabaseText = new TextBox();
            ManualPanel = new TableLayoutPanel();
            JsonStringLabel = new Label();
            ManuelStringText = new TextBox();
            MySQLPanel.SuspendLayout();
            HeaderPanel.SuspendLayout();
            SqlServerPanel.SuspendLayout();
            PostgreSqlPanel.SuspendLayout();
            ManualPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MySQLPanel
            // 
            MySQLPanel.AutoSize = true;
            MySQLPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MySQLPanel.ColumnCount = 2;
            MySQLPanel.ColumnStyles.Add(new ColumnStyle());
            MySQLPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MySQLPanel.Controls.Add(MySqlPortText, 1, 3);
            MySQLPanel.Controls.Add(MySqlPortLabel, 0, 3);
            MySQLPanel.Controls.Add(MySqlTitleLabel, 0, 0);
            MySQLPanel.Controls.Add(MySqlLabel1, 0, 1);
            MySQLPanel.Controls.Add(MySqlServerText, 1, 1);
            MySQLPanel.Controls.Add(MySqlLabel2, 0, 2);
            MySQLPanel.Controls.Add(MySqlDatabaseText, 1, 2);
            MySQLPanel.Controls.Add(MySqlLabel3, 0, 5);
            MySQLPanel.Controls.Add(MySqlUserIDText, 1, 5);
            MySQLPanel.Controls.Add(MySqlLabel4, 0, 6);
            MySQLPanel.Controls.Add(MySqlPasswordText, 1, 6);
            MySQLPanel.Controls.Add(MySqlISLabel, 0, 4);
            MySQLPanel.Controls.Add(MySqlISCheckBox, 1, 4);
            MySQLPanel.Dock = DockStyle.Top;
            MySQLPanel.Location = new Point(0, 31);
            MySQLPanel.Margin = new Padding(15);
            MySQLPanel.Name = "MySQLPanel";
            MySQLPanel.Padding = new Padding(15);
            MySQLPanel.RowCount = 7;
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.RowStyles.Add(new RowStyle());
            MySQLPanel.Size = new Size(562, 215);
            MySQLPanel.TabIndex = 5;
            // 
            // MySqlPortText
            // 
            MySqlPortText.Dock = DockStyle.Fill;
            MySqlPortText.Location = new Point(81, 91);
            MySqlPortText.Name = "MySqlPortText";
            MySqlPortText.Size = new Size(463, 23);
            MySqlPortText.TabIndex = 3;
            // 
            // MySqlPortLabel
            // 
            MySqlPortLabel.AutoSize = true;
            MySqlPortLabel.Dock = DockStyle.Fill;
            MySqlPortLabel.Location = new Point(18, 88);
            MySqlPortLabel.Name = "MySqlPortLabel";
            MySqlPortLabel.Size = new Size(57, 29);
            MySqlPortLabel.TabIndex = 9;
            MySqlPortLabel.Text = "Port";
            MySqlPortLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlTitleLabel
            // 
            MySQLPanel.SetColumnSpan(MySqlTitleLabel, 2);
            MySqlTitleLabel.Dock = DockStyle.Fill;
            MySqlTitleLabel.Location = new Point(18, 15);
            MySqlTitleLabel.Name = "MySqlTitleLabel";
            MySqlTitleLabel.Size = new Size(526, 15);
            MySqlTitleLabel.TabIndex = 3;
            MySqlTitleLabel.Text = "My Sql";
            MySqlTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlLabel1
            // 
            MySqlLabel1.AutoSize = true;
            MySqlLabel1.Dock = DockStyle.Fill;
            MySqlLabel1.Location = new Point(18, 30);
            MySqlLabel1.Name = "MySqlLabel1";
            MySqlLabel1.Size = new Size(57, 29);
            MySqlLabel1.TabIndex = 0;
            MySqlLabel1.Text = "Server";
            MySqlLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlServerText
            // 
            MySqlServerText.Dock = DockStyle.Fill;
            MySqlServerText.Location = new Point(81, 33);
            MySqlServerText.Name = "MySqlServerText";
            MySqlServerText.Size = new Size(463, 23);
            MySqlServerText.TabIndex = 1;
            // 
            // MySqlLabel2
            // 
            MySqlLabel2.AutoSize = true;
            MySqlLabel2.Dock = DockStyle.Fill;
            MySqlLabel2.Location = new Point(18, 59);
            MySqlLabel2.Name = "MySqlLabel2";
            MySqlLabel2.Size = new Size(57, 29);
            MySqlLabel2.TabIndex = 1;
            MySqlLabel2.Text = "Database";
            MySqlLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlDatabaseText
            // 
            MySqlDatabaseText.Dock = DockStyle.Fill;
            MySqlDatabaseText.Location = new Point(81, 62);
            MySqlDatabaseText.Name = "MySqlDatabaseText";
            MySqlDatabaseText.Size = new Size(463, 23);
            MySqlDatabaseText.TabIndex = 2;
            // 
            // MySqlLabel3
            // 
            MySqlLabel3.AutoSize = true;
            MySqlLabel3.Dock = DockStyle.Fill;
            MySqlLabel3.Location = new Point(18, 142);
            MySqlLabel3.Name = "MySqlLabel3";
            MySqlLabel3.Size = new Size(57, 29);
            MySqlLabel3.TabIndex = 4;
            MySqlLabel3.Text = "User ID";
            MySqlLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlUserIDText
            // 
            MySqlUserIDText.Dock = DockStyle.Fill;
            MySqlUserIDText.Location = new Point(81, 145);
            MySqlUserIDText.Name = "MySqlUserIDText";
            MySqlUserIDText.Size = new Size(463, 23);
            MySqlUserIDText.TabIndex = 5;
            // 
            // MySqlLabel4
            // 
            MySqlLabel4.AutoSize = true;
            MySqlLabel4.Dock = DockStyle.Fill;
            MySqlLabel4.Location = new Point(18, 171);
            MySqlLabel4.Name = "MySqlLabel4";
            MySqlLabel4.Size = new Size(57, 29);
            MySqlLabel4.TabIndex = 6;
            MySqlLabel4.Text = "Password";
            MySqlLabel4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlPasswordText
            // 
            MySqlPasswordText.Dock = DockStyle.Fill;
            MySqlPasswordText.Location = new Point(81, 174);
            MySqlPasswordText.Name = "MySqlPasswordText";
            MySqlPasswordText.PasswordChar = '*';
            MySqlPasswordText.ShortcutsEnabled = false;
            MySqlPasswordText.Size = new Size(463, 23);
            MySqlPasswordText.TabIndex = 6;
            // 
            // MySqlISLabel
            // 
            MySqlISLabel.AutoSize = true;
            MySqlISLabel.Dock = DockStyle.Fill;
            MySqlISLabel.Location = new Point(18, 117);
            MySqlISLabel.Name = "MySqlISLabel";
            MySqlISLabel.Size = new Size(57, 25);
            MySqlISLabel.TabIndex = 7;
            MySqlISLabel.Text = "-";
            MySqlISLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MySqlISCheckBox
            // 
            MySqlISCheckBox.AutoSize = true;
            MySqlISCheckBox.Dock = DockStyle.Fill;
            MySqlISCheckBox.Location = new Point(81, 120);
            MySqlISCheckBox.Name = "MySqlISCheckBox";
            MySqlISCheckBox.Size = new Size(463, 19);
            MySqlISCheckBox.TabIndex = 4;
            MySqlISCheckBox.Text = "Integrated Security?";
            MySqlISCheckBox.UseVisualStyleBackColor = true;
            MySqlISCheckBox.CheckedChanged += MySqlIsCheckBox_CheckedChanged;
            // 
            // HeaderPanel
            // 
            HeaderPanel.AutoSize = true;
            HeaderPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            HeaderPanel.Controls.Add(ResetToHardcodedButton);
            HeaderPanel.Controls.Add(SaveButton);
            HeaderPanel.Controls.Add(connectionStringChoice);
            HeaderPanel.Controls.Add(ServerTypeLabel);
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.Name = "HeaderPanel";
            HeaderPanel.Size = new Size(562, 31);
            HeaderPanel.TabIndex = 6;
            // 
            // ResetToHardcodedButton
            // 
            ResetToHardcodedButton.Location = new Point(388, 5);
            ResetToHardcodedButton.Name = "ResetToHardcodedButton";
            ResetToHardcodedButton.Size = new Size(75, 23);
            ResetToHardcodedButton.TabIndex = 3;
            ResetToHardcodedButton.Text = "Default";
            ResetToHardcodedButton.UseVisualStyleBackColor = true;
            ResetToHardcodedButton.Click += ResetToHardcoded_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveButton.AutoSize = true;
            SaveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SaveButton.Location = new Point(469, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(90, 25);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "Save Changes";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // connectionStringChoice
            // 
            connectionStringChoice.AutoCompleteMode = AutoCompleteMode.Suggest;
            connectionStringChoice.AutoCompleteSource = AutoCompleteSource.ListItems;
            connectionStringChoice.DropDownStyle = ComboBoxStyle.DropDownList;
            connectionStringChoice.FormattingEnabled = true;
            connectionStringChoice.Items.AddRange(new object[] { "MySQL", "SQL Server", "POSTgresSQL", "Manual (or Json)" });
            connectionStringChoice.Location = new Point(77, 3);
            connectionStringChoice.Name = "connectionStringChoice";
            connectionStringChoice.Size = new Size(200, 23);
            connectionStringChoice.TabIndex = 0;
            connectionStringChoice.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // ServerTypeLabel
            // 
            ServerTypeLabel.AutoSize = true;
            ServerTypeLabel.Location = new Point(7, 7);
            ServerTypeLabel.Margin = new Padding(7);
            ServerTypeLabel.Name = "ServerTypeLabel";
            ServerTypeLabel.Size = new Size(63, 15);
            ServerTypeLabel.TabIndex = 1;
            ServerTypeLabel.Text = "ServerType";
            ServerTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SqlServerPanel
            // 
            SqlServerPanel.AutoSize = true;
            SqlServerPanel.ColumnCount = 2;
            SqlServerPanel.ColumnStyles.Add(new ColumnStyle());
            SqlServerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            SqlServerPanel.Controls.Add(MSSqlPortText, 1, 3);
            SqlServerPanel.Controls.Add(MSSqlPortLabel, 0, 3);
            SqlServerPanel.Controls.Add(MSSqlTitleLabel, 0, 0);
            SqlServerPanel.Controls.Add(MSSqlServerLabel, 0, 1);
            SqlServerPanel.Controls.Add(MSSqlServerText, 1, 1);
            SqlServerPanel.Controls.Add(MSSqlDatabaseLabel, 0, 2);
            SqlServerPanel.Controls.Add(MSSqlDatabaseText, 1, 2);
            SqlServerPanel.Controls.Add(MSSqlISLabel, 0, 4);
            SqlServerPanel.Controls.Add(MSSqlISCheckBox, 1, 4);
            SqlServerPanel.Controls.Add(MSSqlUserIDLabel, 0, 5);
            SqlServerPanel.Controls.Add(MSSqlUserIDText, 1, 5);
            SqlServerPanel.Controls.Add(MSSqlPasswordLabel, 0, 6);
            SqlServerPanel.Controls.Add(MSSqlPasswordText, 1, 6);
            SqlServerPanel.Dock = DockStyle.Top;
            SqlServerPanel.Location = new Point(0, 246);
            SqlServerPanel.Margin = new Padding(15);
            SqlServerPanel.Name = "SqlServerPanel";
            SqlServerPanel.Padding = new Padding(15);
            SqlServerPanel.RowCount = 7;
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            SqlServerPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            SqlServerPanel.Size = new Size(562, 224);
            SqlServerPanel.TabIndex = 7;
            // 
            // MSSqlPortText
            // 
            MSSqlPortText.Dock = DockStyle.Fill;
            MSSqlPortText.Location = new Point(81, 105);
            MSSqlPortText.Name = "MSSqlPortText";
            MSSqlPortText.Size = new Size(463, 23);
            MSSqlPortText.TabIndex = 15;
            // 
            // MSSqlPortLabel
            // 
            MSSqlPortLabel.AutoSize = true;
            MSSqlPortLabel.Dock = DockStyle.Fill;
            MSSqlPortLabel.Location = new Point(18, 102);
            MSSqlPortLabel.Name = "MSSqlPortLabel";
            MSSqlPortLabel.Size = new Size(57, 29);
            MSSqlPortLabel.TabIndex = 14;
            MSSqlPortLabel.Text = "Port";
            MSSqlPortLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlTitleLabel
            // 
            SqlServerPanel.SetColumnSpan(MSSqlTitleLabel, 2);
            MSSqlTitleLabel.Dock = DockStyle.Fill;
            MSSqlTitleLabel.Location = new Point(18, 15);
            MSSqlTitleLabel.Name = "MSSqlTitleLabel";
            MSSqlTitleLabel.Size = new Size(526, 29);
            MSSqlTitleLabel.TabIndex = 4;
            MSSqlTitleLabel.Text = "MSSql or Microsoft SQL";
            MSSqlTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlServerLabel
            // 
            MSSqlServerLabel.AutoSize = true;
            MSSqlServerLabel.Dock = DockStyle.Fill;
            MSSqlServerLabel.Location = new Point(18, 44);
            MSSqlServerLabel.Name = "MSSqlServerLabel";
            MSSqlServerLabel.Size = new Size(57, 29);
            MSSqlServerLabel.TabIndex = 1;
            MSSqlServerLabel.Text = "Server";
            MSSqlServerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlServerText
            // 
            MSSqlServerText.Dock = DockStyle.Fill;
            MSSqlServerText.Location = new Point(81, 47);
            MSSqlServerText.Name = "MSSqlServerText";
            MSSqlServerText.Size = new Size(463, 23);
            MSSqlServerText.TabIndex = 0;
            // 
            // MSSqlDatabaseLabel
            // 
            MSSqlDatabaseLabel.AutoSize = true;
            MSSqlDatabaseLabel.Dock = DockStyle.Fill;
            MSSqlDatabaseLabel.Location = new Point(18, 73);
            MSSqlDatabaseLabel.Name = "MSSqlDatabaseLabel";
            MSSqlDatabaseLabel.Size = new Size(57, 29);
            MSSqlDatabaseLabel.TabIndex = 0;
            MSSqlDatabaseLabel.Text = "Database";
            MSSqlDatabaseLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlDatabaseText
            // 
            MSSqlDatabaseText.Dock = DockStyle.Fill;
            MSSqlDatabaseText.Location = new Point(81, 76);
            MSSqlDatabaseText.Name = "MSSqlDatabaseText";
            MSSqlDatabaseText.Size = new Size(463, 23);
            MSSqlDatabaseText.TabIndex = 3;
            // 
            // MSSqlISLabel
            // 
            MSSqlISLabel.AutoSize = true;
            MSSqlISLabel.Dock = DockStyle.Fill;
            MSSqlISLabel.Location = new Point(18, 131);
            MSSqlISLabel.Name = "MSSqlISLabel";
            MSSqlISLabel.Size = new Size(57, 29);
            MSSqlISLabel.TabIndex = 8;
            MSSqlISLabel.Text = "-";
            MSSqlISLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlISCheckBox
            // 
            MSSqlISCheckBox.AutoSize = true;
            MSSqlISCheckBox.Dock = DockStyle.Fill;
            MSSqlISCheckBox.Location = new Point(81, 134);
            MSSqlISCheckBox.Name = "MSSqlISCheckBox";
            MSSqlISCheckBox.Size = new Size(463, 23);
            MSSqlISCheckBox.TabIndex = 9;
            MSSqlISCheckBox.Text = "Integrated Security?";
            MSSqlISCheckBox.UseVisualStyleBackColor = true;
            MSSqlISCheckBox.CheckedChanged += MSSQLISCheckBox_CheckedChanged;
            // 
            // MSSqlUserIDLabel
            // 
            MSSqlUserIDLabel.AutoSize = true;
            MSSqlUserIDLabel.Dock = DockStyle.Fill;
            MSSqlUserIDLabel.Location = new Point(18, 160);
            MSSqlUserIDLabel.Name = "MSSqlUserIDLabel";
            MSSqlUserIDLabel.Size = new Size(57, 29);
            MSSqlUserIDLabel.TabIndex = 10;
            MSSqlUserIDLabel.Text = "User ID";
            MSSqlUserIDLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlUserIDText
            // 
            MSSqlUserIDText.Dock = DockStyle.Fill;
            MSSqlUserIDText.Location = new Point(81, 163);
            MSSqlUserIDText.Name = "MSSqlUserIDText";
            MSSqlUserIDText.Size = new Size(463, 23);
            MSSqlUserIDText.TabIndex = 11;
            // 
            // MSSqlPasswordLabel
            // 
            MSSqlPasswordLabel.AutoSize = true;
            MSSqlPasswordLabel.Dock = DockStyle.Fill;
            MSSqlPasswordLabel.Location = new Point(18, 189);
            MSSqlPasswordLabel.Name = "MSSqlPasswordLabel";
            MSSqlPasswordLabel.Size = new Size(57, 20);
            MSSqlPasswordLabel.TabIndex = 12;
            MSSqlPasswordLabel.Text = "Password";
            MSSqlPasswordLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MSSqlPasswordText
            // 
            MSSqlPasswordText.Dock = DockStyle.Fill;
            MSSqlPasswordText.Location = new Point(81, 192);
            MSSqlPasswordText.Name = "MSSqlPasswordText";
            MSSqlPasswordText.PasswordChar = '*';
            MSSqlPasswordText.ShortcutsEnabled = false;
            MSSqlPasswordText.Size = new Size(463, 23);
            MSSqlPasswordText.TabIndex = 13;
            // 
            // PostgreSqlPanel
            // 
            PostgreSqlPanel.AutoSize = true;
            PostgreSqlPanel.ColumnCount = 2;
            PostgreSqlPanel.ColumnStyles.Add(new ColumnStyle());
            PostgreSqlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            PostgreSqlPanel.Controls.Add(PostgresPasswordText, 1, 5);
            PostgreSqlPanel.Controls.Add(label6, 0, 5);
            PostgreSqlPanel.Controls.Add(PostgresUserIDText, 1, 4);
            PostgreSqlPanel.Controls.Add(label5, 0, 4);
            PostgreSqlPanel.Controls.Add(PostgresPortText, 1, 3);
            PostgreSqlPanel.Controls.Add(label2, 0, 3);
            PostgreSqlPanel.Controls.Add(label1, 0, 0);
            PostgreSqlPanel.Controls.Add(label3, 0, 1);
            PostgreSqlPanel.Controls.Add(PostgresHostText, 1, 1);
            PostgreSqlPanel.Controls.Add(label4, 0, 2);
            PostgreSqlPanel.Controls.Add(PostgresDatabaseText, 1, 2);
            PostgreSqlPanel.Dock = DockStyle.Top;
            PostgreSqlPanel.Location = new Point(0, 470);
            PostgreSqlPanel.Margin = new Padding(15);
            PostgreSqlPanel.Name = "PostgreSqlPanel";
            PostgreSqlPanel.Padding = new Padding(15);
            PostgreSqlPanel.RowCount = 6;
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            PostgreSqlPanel.Size = new Size(562, 204);
            PostgreSqlPanel.TabIndex = 8;
            // 
            // PostgresPasswordText
            // 
            PostgresPasswordText.Dock = DockStyle.Fill;
            PostgresPasswordText.Location = new Point(81, 163);
            PostgresPasswordText.Name = "PostgresPasswordText";
            PostgresPasswordText.Size = new Size(463, 23);
            PostgresPasswordText.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(18, 160);
            label6.Name = "label6";
            label6.Size = new Size(57, 29);
            label6.TabIndex = 9;
            label6.Text = "Password";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PostgresUserIDText
            // 
            PostgresUserIDText.Dock = DockStyle.Fill;
            PostgresUserIDText.Location = new Point(81, 134);
            PostgresUserIDText.Name = "PostgresUserIDText";
            PostgresUserIDText.Size = new Size(463, 23);
            PostgresUserIDText.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(18, 131);
            label5.Name = "label5";
            label5.Size = new Size(57, 29);
            label5.TabIndex = 7;
            label5.Text = "User ID";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PostgresPortText
            // 
            PostgresPortText.Dock = DockStyle.Fill;
            PostgresPortText.Location = new Point(81, 105);
            PostgresPortText.Name = "PostgresPortText";
            PostgresPortText.Size = new Size(463, 23);
            PostgresPortText.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(18, 102);
            label2.Name = "label2";
            label2.Size = new Size(57, 29);
            label2.TabIndex = 5;
            label2.Text = "Port";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            PostgreSqlPanel.SetColumnSpan(label1, 2);
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(18, 15);
            label1.Name = "label1";
            label1.Size = new Size(526, 29);
            label1.TabIndex = 4;
            label1.Text = "Postgres";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(18, 44);
            label3.Name = "label3";
            label3.Size = new Size(57, 29);
            label3.TabIndex = 0;
            label3.Text = "Host";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PostgresHostText
            // 
            PostgresHostText.Dock = DockStyle.Fill;
            PostgresHostText.Location = new Point(81, 47);
            PostgresHostText.Name = "PostgresHostText";
            PostgresHostText.Size = new Size(463, 23);
            PostgresHostText.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(18, 73);
            label4.Name = "label4";
            label4.Size = new Size(57, 29);
            label4.TabIndex = 1;
            label4.Text = "Database";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PostgresDatabaseText
            // 
            PostgresDatabaseText.Dock = DockStyle.Fill;
            PostgresDatabaseText.Location = new Point(81, 76);
            PostgresDatabaseText.Name = "PostgresDatabaseText";
            PostgresDatabaseText.Size = new Size(463, 23);
            PostgresDatabaseText.TabIndex = 3;
            // 
            // ManualPanel
            // 
            ManualPanel.AutoSize = true;
            ManualPanel.ColumnCount = 2;
            ManualPanel.ColumnStyles.Add(new ColumnStyle());
            ManualPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ManualPanel.ColumnStyles.Add(new ColumnStyle());
            ManualPanel.Controls.Add(JsonStringLabel, 0, 0);
            ManualPanel.Controls.Add(ManuelStringText, 1, 0);
            ManualPanel.Dock = DockStyle.Top;
            ManualPanel.Location = new Point(0, 674);
            ManualPanel.Margin = new Padding(15);
            ManualPanel.Name = "ManualPanel";
            ManualPanel.Padding = new Padding(15);
            ManualPanel.RowCount = 1;
            ManualPanel.RowStyles.Add(new RowStyle());
            ManualPanel.RowStyles.Add(new RowStyle());
            ManualPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ManualPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ManualPanel.Size = new Size(562, 59);
            ManualPanel.TabIndex = 9;
            // 
            // JsonStringLabel
            // 
            JsonStringLabel.AutoSize = true;
            JsonStringLabel.Dock = DockStyle.Fill;
            JsonStringLabel.Location = new Point(18, 15);
            JsonStringLabel.Name = "JsonStringLabel";
            JsonStringLabel.Size = new Size(38, 29);
            JsonStringLabel.TabIndex = 0;
            JsonStringLabel.Text = "String";
            JsonStringLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ManuelStringText
            // 
            ManuelStringText.Dock = DockStyle.Fill;
            ManuelStringText.Location = new Point(62, 18);
            ManuelStringText.Name = "ManuelStringText";
            ManuelStringText.Size = new Size(482, 23);
            ManuelStringText.TabIndex = 0;
            // 
            // DatabaseSelecterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 248);
            Controls.Add(ManualPanel);
            Controls.Add(PostgreSqlPanel);
            Controls.Add(SqlServerPanel);
            Controls.Add(MySQLPanel);
            Controls.Add(HeaderPanel);
            Name = "DatabaseSelecterForm";
            Text = "DatabaseSelecterForm";
            Load += DatabaseSelecterForm_Load;
            MySQLPanel.ResumeLayout(false);
            MySQLPanel.PerformLayout();
            HeaderPanel.ResumeLayout(false);
            HeaderPanel.PerformLayout();
            SqlServerPanel.ResumeLayout(false);
            SqlServerPanel.PerformLayout();
            PostgreSqlPanel.ResumeLayout(false);
            PostgreSqlPanel.PerformLayout();
            ManualPanel.ResumeLayout(false);
            ManualPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel MySQLPanel;
        private Label MySqlLabel1;
        private TextBox MySqlServerText;
        private Label MySqlLabel2;
        private TextBox MySqlDatabaseText;
        private Panel HeaderPanel;
        private ComboBox connectionStringChoice;
        private Label ServerTypeLabel;
        private Button SaveButton;
        private TableLayoutPanel SqlServerPanel;
        private Label MSSqlDatabaseLabel;
        private TextBox MSSqlServerText;
        private Label MSSqlServerLabel;
        private TextBox MSSqlDatabaseText;
        private TableLayoutPanel PostgreSqlPanel;
        private Label label3;
        private TextBox PostgresHostText;
        private Label label4;
        private TextBox PostgresDatabaseText;
        private TableLayoutPanel ManualPanel;
        private Label JsonStringLabel;
        private TextBox ManuelStringText;
        private Label MySqlLabel3;
        private TextBox MySqlUserIDText;
        private Label MySqlLabel4;
        private TextBox MySqlPasswordText;
        private Label MySqlISLabel;
        private CheckBox MySqlISCheckBox;
        private Label MySqlTitleLabel;
        private Label MSSqlTitleLabel;
        private TextBox MSSqlPasswordText;
        private Label MSSqlPasswordLabel;
        private TextBox MSSqlUserIDText;
        private Label MSSqlUserIDLabel;
        private CheckBox MSSqlISCheckBox;
        private Label MSSqlISLabel;
        private TextBox MySqlPortText;
        private Label MySqlPortLabel;
        private TextBox MSSqlPortText;
        private Label MSSqlPortLabel;
        private Label label1;
        private TextBox PostgresPasswordText;
        private Label label6;
        private TextBox PostgresUserIDText;
        private Label label5;
        private TextBox PostgresPortText;
        private Label label2;
        private Button ResetToHardcodedButton;
    }
}