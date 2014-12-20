using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;
using System.Reflection;
using System.IO;

namespace GrabTheMoment
{
    public partial class Main : Form
    {
        public enum CopyType
        {
            Disabled,
            Local,
            FTP,
            Dropbox,
            ImgurAnon
        }

        public Main()
        {
            InitializeComponent();
            Log.LogPath = string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM"));
            ConfigLoad();

            VersionLabel2.Text = GtmVersion();
        }

        public static string WhatClipboard()
        {
            return ((CopyType)Settings.Default.CopyLink).ToString();
        }

        private void ConfigLoad()
        {
            localToolStripMenuItem.Enabled = LocalClipboard.Enabled = Local.Checked = Settings.Default.MLocal;
            fTPToolStripMenuItem.Enabled = FtpClipboard.Enabled = Ftp.Checked = Settings.Default.MFtp;
            dropboxToolStripMenuItem.Enabled = DropboxClipboard.Enabled = Dropbox.Checked = Settings.Default.MDropbox;
            ImgurClipboard.Enabled = ImgurAnon.Checked = Settings.Default.MImgur;

            switch ((CopyType)Settings.Default.CopyLink)
            {
                case CopyType.Local:
                    LocalClipboard.Checked = true;
                    break;
                case CopyType.FTP:
                    FtpClipboard.Checked = true;
                    break;
                case CopyType.Dropbox:
                    DropboxClipboard.Checked = true;
                    break;
                case CopyType.ImgurAnon:
                    ImgurClipboard.Checked = true;
                    break;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                //this.Activate();
            }
            //else
            //{
            //    this.ShowInTaskbar = false;
            //    //this.Hide();
            //    this.WindowState = FormWindowState.Minimized;
            //    this.WindowState = FormWindowState.Normal;

            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(Control.MousePosition);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.local localForm = new Savemode.Forms.local();
            localForm.Show();
        }

        private void LocalEnable(object sender, EventArgs e)
        {
            Settings.Default.MLocal = LocalClipboard.Enabled = localToolStripMenuItem.Enabled = Local.Checked;

            if (!LocalClipboard.Enabled)
                LocalClipboard.Checked = false;

            Settings.Default.Save();
        }

        private void FtpEnable(object sender, EventArgs e)
        {
            Settings.Default.MFtp = FtpClipboard.Enabled = fTPToolStripMenuItem.Enabled = Ftp.Checked;

            if (!FtpClipboard.Enabled)
                FtpClipboard.Checked = false;

            Settings.Default.Save();
        }

        private void fTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.FTP ftpForm = new Savemode.Forms.FTP();
            ftpForm.Show();
        }

        private void DropboxEnable(object sender, EventArgs e)
        {
            Settings.Default.MDropbox = DropboxClipboard.Enabled = dropboxToolStripMenuItem.Enabled = Dropbox.Checked;

            if (!DropboxClipboard.Enabled)
                DropboxClipboard.Checked = false;

            Settings.Default.Save();
        }

        private void dropboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.Dropbox dropboxForm = new Savemode.Forms.Dropbox();
            dropboxForm.Show();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MImgur = ImgurClipboard.Enabled = ImgurAnon.Checked;

            if (!ImgurClipboard.Enabled)
                ImgurClipboard.Checked = false;

            Settings.Default.Save();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (LocalClipboard.Checked)
            {
                FtpClipboard.Checked = false;
                DropboxClipboard.Checked = false;
                ImgurClipboard.Checked = false;
                SetClipboardType(CopyType.Local);
            }
            else
                SetClipboardType(CopyType.Disabled);
        }

        private void FtpClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (FtpClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                DropboxClipboard.Checked = false;
                ImgurClipboard.Checked = false;
                SetClipboardType(CopyType.FTP);
            }
            else
                SetClipboardType(CopyType.Disabled);
        }

        private void DropboxClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (DropboxClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                FtpClipboard.Checked = false;
                ImgurClipboard.Checked = false;
                SetClipboardType(CopyType.Dropbox);
            }
            else
                SetClipboardType(CopyType.Disabled);
        }

        private void ImgurClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (ImgurClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                FtpClipboard.Checked = false;
                DropboxClipboard.Checked = false;
                SetClipboardType(CopyType.ImgurAnon);
            }
            else
                SetClipboardType(CopyType.Disabled);
        }

        private void lastLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }

        // Beallitja a CopyLink erteket
        private void SetClipboardType(CopyType Type)
        {
            Settings.Default.CopyLink = (int)Type;
            Settings.Default.Save();
        }

        private string GtmVersion()
        {
            string version = String.Empty;

            string version_file = Assembly.GetCallingAssembly().GetName().Name + ".Properties..gtm-version";

            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(version_file))
            using (StreamReader reader = new StreamReader(stream))
            {
                version = reader.ReadToEnd();
            }

            return version;
        }
    }
}
