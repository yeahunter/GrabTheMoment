namespace GrabTheMoment.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lastLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Local = new System.Windows.Forms.CheckBox();
            this.Ftp = new System.Windows.Forms.CheckBox();
            this.Dropbox = new System.Windows.Forms.CheckBox();
            this.ImgurAnon = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LocalClipboard = new System.Windows.Forms.CheckBox();
            this.FtpClipboard = new System.Windows.Forms.CheckBox();
            this.DropboxClipboard = new System.Windows.Forms.CheckBox();
            this.ImgurClipboard = new System.Windows.Forms.CheckBox();
            this.VersionLabel1 = new System.Windows.Forms.Label();
            this.VersionLabel2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Grab The Moment - Developed by Invisible © 2013";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.notifyIcon1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastLinkToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // lastLinkToolStripMenuItem
            // 
            this.lastLinkToolStripMenuItem.Enabled = false;
            this.lastLinkToolStripMenuItem.Name = "lastLinkToolStripMenuItem";
            this.lastLinkToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.lastLinkToolStripMenuItem.Text = "Last link";
            this.lastLinkToolStripMenuItem.Click += new System.EventHandler(this.lastLinkToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.saveModeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(260, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveModeToolStripMenuItem
            // 
            this.saveModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localToolStripMenuItem,
            this.fTPToolStripMenuItem,
            this.dropboxToolStripMenuItem});
            this.saveModeToolStripMenuItem.Name = "saveModeToolStripMenuItem";
            this.saveModeToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.saveModeToolStripMenuItem.Text = "Save mode";
            // 
            // localToolStripMenuItem
            // 
            this.localToolStripMenuItem.Enabled = false;
            this.localToolStripMenuItem.Name = "localToolStripMenuItem";
            this.localToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.localToolStripMenuItem.Text = "Local";
            this.localToolStripMenuItem.Click += new System.EventHandler(this.localToolStripMenuItem_Click);
            // 
            // fTPToolStripMenuItem
            // 
            this.fTPToolStripMenuItem.Enabled = false;
            this.fTPToolStripMenuItem.Name = "fTPToolStripMenuItem";
            this.fTPToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.fTPToolStripMenuItem.Text = "FTP";
            this.fTPToolStripMenuItem.Click += new System.EventHandler(this.fTPToolStripMenuItem_Click);
            // 
            // dropboxToolStripMenuItem
            // 
            this.dropboxToolStripMenuItem.Enabled = false;
            this.dropboxToolStripMenuItem.Name = "dropboxToolStripMenuItem";
            this.dropboxToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.dropboxToolStripMenuItem.Text = "Dropbox";
            this.dropboxToolStripMenuItem.Click += new System.EventHandler(this.dropboxToolStripMenuItem_Click);
            // 
            // Local
            // 
            this.Local.AutoSize = true;
            this.Local.Location = new System.Drawing.Point(12, 47);
            this.Local.Name = "Local";
            this.Local.Size = new System.Drawing.Size(52, 17);
            this.Local.TabIndex = 5;
            this.Local.Text = "Local";
            this.Local.UseVisualStyleBackColor = true;
            this.Local.CheckedChanged += new System.EventHandler(this.LocalEnable);
            // 
            // Ftp
            // 
            this.Ftp.AutoSize = true;
            this.Ftp.Location = new System.Drawing.Point(12, 70);
            this.Ftp.Name = "Ftp";
            this.Ftp.Size = new System.Drawing.Size(46, 17);
            this.Ftp.TabIndex = 6;
            this.Ftp.Text = "FTP";
            this.Ftp.UseVisualStyleBackColor = true;
            this.Ftp.CheckedChanged += new System.EventHandler(this.FtpEnable);
            // 
            // Dropbox
            // 
            this.Dropbox.AutoSize = true;
            this.Dropbox.Location = new System.Drawing.Point(12, 93);
            this.Dropbox.Name = "Dropbox";
            this.Dropbox.Size = new System.Drawing.Size(66, 17);
            this.Dropbox.TabIndex = 7;
            this.Dropbox.Text = "Dropbox";
            this.Dropbox.UseVisualStyleBackColor = true;
            this.Dropbox.CheckedChanged += new System.EventHandler(this.DropboxEnable);
            // 
            // ImgurAnon
            // 
            this.ImgurAnon.AutoSize = true;
            this.ImgurAnon.Location = new System.Drawing.Point(12, 116);
            this.ImgurAnon.Name = "ImgurAnon";
            this.ImgurAnon.Size = new System.Drawing.Size(110, 17);
            this.ImgurAnon.TabIndex = 10;
            this.ImgurAnon.Text = "Imgur (Anonymus)";
            this.ImgurAnon.UseVisualStyleBackColor = true;
            this.ImgurAnon.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "ON/OFF";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Save to clipboard";
            // 
            // LocalClipboard
            // 
            this.LocalClipboard.AutoSize = true;
            this.LocalClipboard.Enabled = false;
            this.LocalClipboard.Location = new System.Drawing.Point(139, 48);
            this.LocalClipboard.Name = "LocalClipboard";
            this.LocalClipboard.Size = new System.Drawing.Size(15, 14);
            this.LocalClipboard.TabIndex = 13;
            this.LocalClipboard.UseVisualStyleBackColor = true;
            this.LocalClipboard.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // FtpClipboard
            // 
            this.FtpClipboard.AutoSize = true;
            this.FtpClipboard.Enabled = false;
            this.FtpClipboard.Location = new System.Drawing.Point(139, 71);
            this.FtpClipboard.Name = "FtpClipboard";
            this.FtpClipboard.Size = new System.Drawing.Size(15, 14);
            this.FtpClipboard.TabIndex = 14;
            this.FtpClipboard.UseVisualStyleBackColor = true;
            this.FtpClipboard.CheckedChanged += new System.EventHandler(this.FtpClipboard_CheckedChanged);
            // 
            // DropboxClipboard
            // 
            this.DropboxClipboard.AutoSize = true;
            this.DropboxClipboard.Enabled = false;
            this.DropboxClipboard.Location = new System.Drawing.Point(139, 94);
            this.DropboxClipboard.Name = "DropboxClipboard";
            this.DropboxClipboard.Size = new System.Drawing.Size(15, 14);
            this.DropboxClipboard.TabIndex = 15;
            this.DropboxClipboard.UseVisualStyleBackColor = true;
            this.DropboxClipboard.CheckedChanged += new System.EventHandler(this.DropboxClipboard_CheckedChanged);
            // 
            // ImgurClipboard
            // 
            this.ImgurClipboard.AutoSize = true;
            this.ImgurClipboard.Enabled = false;
            this.ImgurClipboard.Location = new System.Drawing.Point(139, 117);
            this.ImgurClipboard.Name = "ImgurClipboard";
            this.ImgurClipboard.Size = new System.Drawing.Size(15, 14);
            this.ImgurClipboard.TabIndex = 16;
            this.ImgurClipboard.UseVisualStyleBackColor = true;
            this.ImgurClipboard.CheckedChanged += new System.EventHandler(this.ImgurClipboard_CheckedChanged);
            // 
            // VersionLabel1
            // 
            this.VersionLabel1.AutoSize = true;
            this.VersionLabel1.Location = new System.Drawing.Point(12, 140);
            this.VersionLabel1.Name = "VersionLabel1";
            this.VersionLabel1.Size = new System.Drawing.Size(45, 13);
            this.VersionLabel1.TabIndex = 17;
            this.VersionLabel1.Text = "Version:";
            // 
            // VersionLabel2
            // 
            this.VersionLabel2.AutoSize = true;
            this.VersionLabel2.Location = new System.Drawing.Point(54, 140);
            this.VersionLabel2.Name = "VersionLabel2";
            this.VersionLabel2.Size = new System.Drawing.Size(0, 13);
            this.VersionLabel2.TabIndex = 18;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 163);
            this.Controls.Add(this.VersionLabel2);
            this.Controls.Add(this.VersionLabel1);
            this.Controls.Add(this.ImgurClipboard);
            this.Controls.Add(this.DropboxClipboard);
            this.Controls.Add(this.FtpClipboard);
            this.Controls.Add(this.LocalClipboard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImgurAnon);
            this.Controls.Add(this.Dropbox);
            this.Controls.Add(this.Ftp);
            this.Controls.Add(this.Local);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.Text = "Grab The Moment";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localToolStripMenuItem;
        private System.Windows.Forms.CheckBox Local;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox Ftp;
        private System.Windows.Forms.CheckBox Dropbox;
        private System.Windows.Forms.ToolStripMenuItem fTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dropboxToolStripMenuItem;
        private System.Windows.Forms.CheckBox ImgurAnon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox LocalClipboard;
        private System.Windows.Forms.CheckBox FtpClipboard;
        private System.Windows.Forms.CheckBox DropboxClipboard;
        private System.Windows.Forms.CheckBox ImgurClipboard;
        public System.Windows.Forms.ToolStripMenuItem lastLinkToolStripMenuItem;
        private System.Windows.Forms.Label VersionLabel1;
        private System.Windows.Forms.Label VersionLabel2;
    }
}

