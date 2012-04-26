// Copyright (c) 2012 Integryst, LLC, http://www.integryst.com/
// See LICENSE.txt for licensing information

namespace UserAuthSourceFlipper
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
            this.lstUsers = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSwapSelectedGroups = new System.Windows.Forms.Button();
            this.btnLoadGroups = new System.Windows.Forms.Button();
            this.btnSwapSelected = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.btnLoadUsers = new System.Windows.Forms.Button();
            this.cmbRightAuthSource = new System.Windows.Forms.ComboBox();
            this.cmbLeftAuthSource = new System.Windows.Forms.ComboBox();
            this.btnLoadAuthSources = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDBPass = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDBPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.rdoOracle = new System.Windows.Forms.RadioButton();
            this.rdoSQLServer = new System.Windows.Forms.RadioButton();
            this.txtDBServer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstUsers
            // 
            this.lstUsers.FullRowSelect = true;
            this.lstUsers.GridLines = true;
            this.lstUsers.Location = new System.Drawing.Point(6, 98);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(765, 502);
            this.lstUsers.TabIndex = 11;
            this.lstUsers.UseCompatibleStateImageBehavior = false;
            this.lstUsers.View = System.Windows.Forms.View.Details;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSwapSelectedGroups);
            this.groupBox1.Controls.Add(this.btnLoadGroups);
            this.groupBox1.Controls.Add(this.btnSwapSelected);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.prgStatus);
            this.groupBox1.Controls.Add(this.btnLoadUsers);
            this.groupBox1.Controls.Add(this.cmbRightAuthSource);
            this.groupBox1.Controls.Add(this.cmbLeftAuthSource);
            this.groupBox1.Controls.Add(this.btnLoadAuthSources);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDBName);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDBPass);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtDBUser);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtDBPort);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.rdoOracle);
            this.groupBox1.Controls.Add(this.rdoSQLServer);
            this.groupBox1.Controls.Add(this.txtDBServer);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(977, 101);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnSwapSelectedGroups
            // 
            this.btnSwapSelectedGroups.Location = new System.Drawing.Point(879, 69);
            this.btnSwapSelectedGroups.Name = "btnSwapSelectedGroups";
            this.btnSwapSelectedGroups.Size = new System.Drawing.Size(88, 23);
            this.btnSwapSelectedGroups.TabIndex = 73;
            this.btnSwapSelectedGroups.Text = "Swap Selected";
            this.btnSwapSelectedGroups.UseVisualStyleBackColor = true;
            this.btnSwapSelectedGroups.Click += new System.EventHandler(this.btnSwapSelectedGroups_Click);
            // 
            // btnLoadGroups
            // 
            this.btnLoadGroups.Location = new System.Drawing.Point(786, 69);
            this.btnLoadGroups.Name = "btnLoadGroups";
            this.btnLoadGroups.Size = new System.Drawing.Size(87, 23);
            this.btnLoadGroups.TabIndex = 72;
            this.btnLoadGroups.Text = "load Groups";
            this.btnLoadGroups.UseVisualStyleBackColor = true;
            this.btnLoadGroups.Click += new System.EventHandler(this.btnLoadGroups_Click);
            // 
            // btnSwapSelected
            // 
            this.btnSwapSelected.Location = new System.Drawing.Point(879, 41);
            this.btnSwapSelected.Name = "btnSwapSelected";
            this.btnSwapSelected.Size = new System.Drawing.Size(88, 23);
            this.btnSwapSelected.TabIndex = 71;
            this.btnSwapSelected.Text = "Swap Selected";
            this.btnSwapSelected.UseVisualStyleBackColor = true;
            this.btnSwapSelected.Click += new System.EventHandler(this.btnSwapSelected_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(879, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export List";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // prgStatus
            // 
            this.prgStatus.Location = new System.Drawing.Point(496, 64);
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(254, 23);
            this.prgStatus.TabIndex = 70;
            // 
            // btnLoadUsers
            // 
            this.btnLoadUsers.Location = new System.Drawing.Point(786, 41);
            this.btnLoadUsers.Name = "btnLoadUsers";
            this.btnLoadUsers.Size = new System.Drawing.Size(87, 23);
            this.btnLoadUsers.TabIndex = 8;
            this.btnLoadUsers.Text = "load Users";
            this.btnLoadUsers.UseVisualStyleBackColor = true;
            this.btnLoadUsers.Click += new System.EventHandler(this.btnLoadUsers_Click);
            // 
            // cmbRightAuthSource
            // 
            this.cmbRightAuthSource.DropDownWidth = 300;
            this.cmbRightAuthSource.FormattingEnabled = true;
            this.cmbRightAuthSource.Location = new System.Drawing.Point(379, 64);
            this.cmbRightAuthSource.Name = "cmbRightAuthSource";
            this.cmbRightAuthSource.Size = new System.Drawing.Size(104, 21);
            this.cmbRightAuthSource.TabIndex = 10;
            // 
            // cmbLeftAuthSource
            // 
            this.cmbLeftAuthSource.DropDownWidth = 300;
            this.cmbLeftAuthSource.FormattingEnabled = true;
            this.cmbLeftAuthSource.Location = new System.Drawing.Point(132, 64);
            this.cmbLeftAuthSource.Name = "cmbLeftAuthSource";
            this.cmbLeftAuthSource.Size = new System.Drawing.Size(111, 21);
            this.cmbLeftAuthSource.TabIndex = 9;
            // 
            // btnLoadAuthSources
            // 
            this.btnLoadAuthSources.Location = new System.Drawing.Point(786, 12);
            this.btnLoadAuthSources.Name = "btnLoadAuthSources";
            this.btnLoadAuthSources.Size = new System.Drawing.Size(87, 23);
            this.btnLoadAuthSources.TabIndex = 7;
            this.btnLoadAuthSources.Text = "load Auth Sources";
            this.btnLoadAuthSources.UseVisualStyleBackColor = true;
            this.btnLoadAuthSources.Click += new System.EventHandler(this.btnLoadAuthSources_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Right Auth Source:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Left Auth Source:";
            // 
            // txtDBName
            // 
            this.txtDBName.AcceptsReturn = true;
            this.txtDBName.Location = new System.Drawing.Point(270, 38);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(88, 20);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "alidb";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(226, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Name:";
            // 
            // txtDBPass
            // 
            this.txtDBPass.AcceptsReturn = true;
            this.txtDBPass.Location = new System.Drawing.Point(665, 38);
            this.txtDBPass.Name = "txtDBPass";
            this.txtDBPass.PasswordChar = '*';
            this.txtDBPass.Size = new System.Drawing.Size(85, 20);
            this.txtDBPass.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(603, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Password:";
            // 
            // txtDBUser
            // 
            this.txtDBUser.AcceptsReturn = true;
            this.txtDBUser.Location = new System.Drawing.Point(517, 38);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(79, 20);
            this.txtDBUser.TabIndex = 5;
            this.txtDBUser.Text = "alidbuser";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(479, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 58;
            this.label12.Text = "User:";
            // 
            // txtDBPort
            // 
            this.txtDBPort.AcceptsReturn = true;
            this.txtDBPort.Location = new System.Drawing.Point(426, 38);
            this.txtDBPort.Name = "txtDBPort";
            this.txtDBPort.Size = new System.Drawing.Size(47, 20);
            this.txtDBPort.TabIndex = 4;
            this.txtDBPort.Text = "1433";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(391, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Port:";
            // 
            // rdoOracle
            // 
            this.rdoOracle.AutoSize = true;
            this.rdoOracle.Location = new System.Drawing.Point(6, 41);
            this.rdoOracle.Name = "rdoOracle";
            this.rdoOracle.Size = new System.Drawing.Size(56, 17);
            this.rdoOracle.TabIndex = 1;
            this.rdoOracle.Text = "Oracle";
            this.rdoOracle.UseVisualStyleBackColor = true;
            this.rdoOracle.CheckedChanged += new System.EventHandler(this.rdoOracle_CheckedChanged);
            // 
            // rdoSQLServer
            // 
            this.rdoSQLServer.AutoSize = true;
            this.rdoSQLServer.Checked = true;
            this.rdoSQLServer.Location = new System.Drawing.Point(6, 19);
            this.rdoSQLServer.Name = "rdoSQLServer";
            this.rdoSQLServer.Size = new System.Drawing.Size(77, 17);
            this.rdoSQLServer.TabIndex = 0;
            this.rdoSQLServer.TabStop = true;
            this.rdoSQLServer.Text = "SQLServer";
            this.rdoSQLServer.UseVisualStyleBackColor = true;
            this.rdoSQLServer.CheckedChanged += new System.EventHandler(this.rdoSQLServer_CheckedChanged);
            // 
            // txtDBServer
            // 
            this.txtDBServer.AcceptsReturn = true;
            this.txtDBServer.Location = new System.Drawing.Point(132, 38);
            this.txtDBServer.Name = "txtDBServer";
            this.txtDBServer.Size = new System.Drawing.Size(88, 20);
            this.txtDBServer.TabIndex = 2;
            this.txtDBServer.Text = "localhost";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(76, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "DB Server:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(777, 98);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(206, 502);
            this.txtStatus.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 603);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "UserAuthSourceFlipper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDBPass;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDBPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rdoOracle;
        private System.Windows.Forms.RadioButton rdoSQLServer;
        private System.Windows.Forms.TextBox txtDBServer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLoadUsers;
        private System.Windows.Forms.ComboBox cmbRightAuthSource;
        private System.Windows.Forms.ComboBox cmbLeftAuthSource;
        private System.Windows.Forms.Button btnLoadAuthSources;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.ProgressBar prgStatus;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSwapSelected;
        private System.Windows.Forms.Button btnSwapSelectedGroups;
        private System.Windows.Forms.Button btnLoadGroups;
    }
}

