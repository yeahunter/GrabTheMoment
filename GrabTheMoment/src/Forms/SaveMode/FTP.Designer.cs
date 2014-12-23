namespace GrabTheMoment.Forms.Savemode
{
    partial class FTP
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
            this.FtpUser = new System.Windows.Forms.TextBox();
            this.FtpUserLabel = new System.Windows.Forms.Label();
            this.FtpPasswordLabel = new System.Windows.Forms.Label();
            this.FtpServerLabel = new System.Windows.Forms.Label();
            this.FtpDirectoryLabel = new System.Windows.Forms.Label();
            this.UrlLabel = new System.Windows.Forms.Label();
            this.FtpPassword = new System.Windows.Forms.TextBox();
            this.FtpServer = new System.Windows.Forms.TextBox();
            this.FtpDirectory = new System.Windows.Forms.TextBox();
            this.Url = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FtpUser
            // 
            this.FtpUser.Location = new System.Drawing.Point(103, 12);
            this.FtpUser.Name = "FtpUser";
            this.FtpUser.Size = new System.Drawing.Size(100, 20);
            this.FtpUser.TabIndex = 0;
            // 
            // FtpUserLabel
            // 
            this.FtpUserLabel.AutoSize = true;
            this.FtpUserLabel.Location = new System.Drawing.Point(11, 15);
            this.FtpUserLabel.Name = "FtpUserLabel";
            this.FtpUserLabel.Size = new System.Drawing.Size(86, 13);
            this.FtpUserLabel.TabIndex = 1;
            this.FtpUserLabel.Text = "FTP Felhasználó";
            // 
            // FtpPasswordLabel
            // 
            this.FtpPasswordLabel.AutoSize = true;
            this.FtpPasswordLabel.Location = new System.Drawing.Point(11, 41);
            this.FtpPasswordLabel.Name = "FtpPasswordLabel";
            this.FtpPasswordLabel.Size = new System.Drawing.Size(59, 13);
            this.FtpPasswordLabel.TabIndex = 2;
            this.FtpPasswordLabel.Text = "FTP Jelszó";
            // 
            // FtpServerLabel
            // 
            this.FtpServerLabel.AutoSize = true;
            this.FtpServerLabel.Location = new System.Drawing.Point(11, 67);
            this.FtpServerLabel.Name = "FtpServerLabel";
            this.FtpServerLabel.Size = new System.Drawing.Size(66, 13);
            this.FtpServerLabel.TabIndex = 3;
            this.FtpServerLabel.Text = "FTP Szerver";
            // 
            // FtpDirectoryLabel
            // 
            this.FtpDirectoryLabel.AutoSize = true;
            this.FtpDirectoryLabel.Location = new System.Drawing.Point(11, 93);
            this.FtpDirectoryLabel.Name = "FtpDirectoryLabel";
            this.FtpDirectoryLabel.Size = new System.Drawing.Size(62, 13);
            this.FtpDirectoryLabel.TabIndex = 4;
            this.FtpDirectoryLabel.Text = "FTP mappa";
            // 
            // UrlLabel
            // 
            this.UrlLabel.AutoSize = true;
            this.UrlLabel.Location = new System.Drawing.Point(11, 119);
            this.UrlLabel.Name = "UrlLabel";
            this.UrlLabel.Size = new System.Drawing.Size(29, 13);
            this.UrlLabel.TabIndex = 5;
            this.UrlLabel.Text = "URL";
            // 
            // FtpPassword
            // 
            this.FtpPassword.Location = new System.Drawing.Point(103, 38);
            this.FtpPassword.Name = "FtpPassword";
            this.FtpPassword.Size = new System.Drawing.Size(100, 20);
            this.FtpPassword.TabIndex = 6;
            // 
            // FtpServer
            // 
            this.FtpServer.Location = new System.Drawing.Point(103, 64);
            this.FtpServer.Name = "FtpServer";
            this.FtpServer.Size = new System.Drawing.Size(100, 20);
            this.FtpServer.TabIndex = 7;
            // 
            // FtpDirectory
            // 
            this.FtpDirectory.Location = new System.Drawing.Point(103, 90);
            this.FtpDirectory.Name = "FtpDirectory";
            this.FtpDirectory.Size = new System.Drawing.Size(100, 20);
            this.FtpDirectory.TabIndex = 8;
            // 
            // Url
            // 
            this.Url.Location = new System.Drawing.Point(103, 116);
            this.Url.Name = "Url";
            this.Url.Size = new System.Drawing.Size(100, 20);
            this.Url.TabIndex = 9;
            // 
            // Save
            // 
            this.Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Save.Location = new System.Drawing.Point(75, 143);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 10;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.button1_Click);
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 172);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Url);
            this.Controls.Add(this.FtpDirectory);
            this.Controls.Add(this.FtpServer);
            this.Controls.Add(this.FtpPassword);
            this.Controls.Add(this.UrlLabel);
            this.Controls.Add(this.FtpDirectoryLabel);
            this.Controls.Add(this.FtpServerLabel);
            this.Controls.Add(this.FtpPasswordLabel);
            this.Controls.Add(this.FtpUserLabel);
            this.Controls.Add(this.FtpUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FTP";
            this.Text = "FTP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FtpUser;
        private System.Windows.Forms.Label FtpUserLabel;
        private System.Windows.Forms.Label FtpPasswordLabel;
        private System.Windows.Forms.Label FtpServerLabel;
        private System.Windows.Forms.Label FtpDirectoryLabel;
        private System.Windows.Forms.Label UrlLabel;
        private System.Windows.Forms.TextBox FtpPassword;
        private System.Windows.Forms.TextBox FtpServer;
        private System.Windows.Forms.TextBox FtpDirectory;
        private System.Windows.Forms.TextBox Url;
        private System.Windows.Forms.Button Save;
    }
}