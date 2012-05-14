namespace LogViewer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.checkBoxCase = new System.Windows.Forms.CheckBox();
            this.comboBoxAliasSearch = new System.Windows.Forms.ComboBox();
            this.loadProgress = new System.Windows.Forms.ProgressBar();
            this.loadStatusLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimaryAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfAliases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kicks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TmpBans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Polls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermaBanned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KillDeath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxSortBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAscendingDescending = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxCase
            // 
            this.checkBoxCase.AutoSize = true;
            this.checkBoxCase.Location = new System.Drawing.Point(197, 51);
            this.checkBoxCase.Name = "checkBoxCase";
            this.checkBoxCase.Size = new System.Drawing.Size(96, 17);
            this.checkBoxCase.TabIndex = 4;
            this.checkBoxCase.Text = "Case Sensitive";
            this.checkBoxCase.UseVisualStyleBackColor = true;
            // 
            // comboBoxAliasSearch
            // 
            this.comboBoxAliasSearch.FormattingEnabled = true;
            this.comboBoxAliasSearch.Location = new System.Drawing.Point(70, 51);
            this.comboBoxAliasSearch.Name = "comboBoxAliasSearch";
            this.comboBoxAliasSearch.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAliasSearch.TabIndex = 5;
            this.comboBoxAliasSearch.TextUpdate += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // loadProgress
            // 
            this.loadProgress.Location = new System.Drawing.Point(13, 19);
            this.loadProgress.Name = "loadProgress";
            this.loadProgress.Size = new System.Drawing.Size(202, 23);
            this.loadProgress.TabIndex = 6;
            this.loadProgress.Visible = false;
            // 
            // loadStatusLabel
            // 
            this.loadStatusLabel.AutoSize = true;
            this.loadStatusLabel.Location = new System.Drawing.Point(10, 24);
            this.loadStatusLabel.Name = "loadStatusLabel";
            this.loadStatusLabel.Size = new System.Drawing.Size(54, 13);
            this.loadStatusLabel.TabIndex = 7;
            this.loadStatusLabel.Text = "Loading...";
            this.loadStatusLabel.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(881, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.loadToolStripMenuItem.Text = "Load Log Files";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UID,
            this.PrimaryAlias,
            this.NumberOfAliases,
            this.Kicks,
            this.TmpBans,
            this.Polls,
            this.PermaBanned,
            this.KillDeath});
            this.dataGridView1.Location = new System.Drawing.Point(13, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(847, 568);
            this.dataGridView1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.dataGridView1, "Click in the Leftmost column for detailed player information.");
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // UID
            // 
            this.UID.DataPropertyName = "UID";
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            // 
            // PrimaryAlias
            // 
            this.PrimaryAlias.DataPropertyName = "PrimaryAlias";
            this.PrimaryAlias.HeaderText = "PrimaryAlias";
            this.PrimaryAlias.Name = "PrimaryAlias";
            // 
            // NumberOfAliases
            // 
            this.NumberOfAliases.DataPropertyName = "NumberOfAliases";
            this.NumberOfAliases.HeaderText = "Number Of Aliases";
            this.NumberOfAliases.Name = "NumberOfAliases";
            // 
            // Kicks
            // 
            this.Kicks.DataPropertyName = "Kicks";
            this.Kicks.HeaderText = "Kicks";
            this.Kicks.Name = "Kicks";
            // 
            // TmpBans
            // 
            this.TmpBans.DataPropertyName = "TmpBans";
            this.TmpBans.HeaderText = "Temp Bans";
            this.TmpBans.Name = "TmpBans";
            // 
            // Polls
            // 
            this.Polls.DataPropertyName = "KickBanPolls";
            this.Polls.HeaderText = "Kick/Ban Polls";
            this.Polls.Name = "Polls";
            // 
            // PermaBanned
            // 
            this.PermaBanned.DataPropertyName = "PermaBanned";
            this.PermaBanned.HeaderText = "Perma Banned";
            this.PermaBanned.Name = "PermaBanned";
            // 
            // KillDeath
            // 
            this.KillDeath.DataPropertyName = "KillDeath";
            this.KillDeath.HeaderText = "K/D Ratio";
            this.KillDeath.Name = "KillDeath";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Filter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxSortBy
            // 
            this.comboBoxSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortBy.FormattingEnabled = true;
            this.comboBoxSortBy.Items.AddRange(new object[] {
            "Alias",
            "Kicks",
            "Kick/Ban Polls",
            "K/D Ratio",
            "Number Of Aliases",
            "Perma Banned",
            "Temp Bans",
            "UID"});
            this.comboBoxSortBy.Location = new System.Drawing.Point(70, 78);
            this.comboBoxSortBy.Name = "comboBoxSortBy";
            this.comboBoxSortBy.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSortBy.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Alias:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sort By:";
            // 
            // comboBoxAscendingDescending
            // 
            this.comboBoxAscendingDescending.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAscendingDescending.FormattingEnabled = true;
            this.comboBoxAscendingDescending.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.comboBoxAscendingDescending.Location = new System.Drawing.Point(197, 78);
            this.comboBoxAscendingDescending.Name = "comboBoxAscendingDescending";
            this.comboBoxAscendingDescending.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAscendingDescending.TabIndex = 14;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Click in the leftmost column for more player information";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 714);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxAscendingDescending);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSortBy);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.loadStatusLabel);
            this.Controls.Add(this.loadProgress);
            this.Controls.Add(this.comboBoxAliasSearch);
            this.Controls.Add(this.checkBoxCase);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Log Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxCase;
        private System.Windows.Forms.ComboBox comboBoxAliasSearch;
        private System.Windows.Forms.ProgressBar loadProgress;
        private System.Windows.Forms.Label loadStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxSortBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAscendingDescending;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrimaryAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfAliases;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kicks;
        private System.Windows.Forms.DataGridViewTextBoxColumn TmpBans;
        private System.Windows.Forms.DataGridViewTextBoxColumn Polls;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermaBanned;
        private System.Windows.Forms.DataGridViewTextBoxColumn KillDeath;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
    }
}

