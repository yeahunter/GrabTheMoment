using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Log.LogPath = string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM"));
            localToolStripMenuItem.Enabled      = LocalClipboard.Enabled = Local.Checked = Settings.Default.MLocal;
            fTPToolStripMenuItem.Enabled        = FtpClipboard.Enabled = Ftp.Checked = Settings.Default.MFtp;
            dropboxToolStripMenuItem.Enabled    = DropboxClipboard.Enabled = Dropbox.Checked = Settings.Default.MDropbox;
            ImgurClipboard.Enabled = ImgurAnon.Checked = Settings.Default.MImgur;

            switch (Settings.Default.CopyLink)
            {
                case 1:
                    LocalClipboard.Checked = true;
                    break;
                case 2:
                    FtpClipboard.Checked = true;
                    break;
                case 3:
                    DropboxClipboard.Checked = true;
                    break;
                case 4:
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
                SetClipboardType(ScreenMode.allmode.CopyType.Local);
            }
            else
                SetClipboardType(ScreenMode.allmode.CopyType.Disabled);
        }

        private void FtpClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (FtpClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                DropboxClipboard.Checked = false;
                ImgurClipboard.Checked = false;
                SetClipboardType(ScreenMode.allmode.CopyType.FTP);
            }
            else
                SetClipboardType(ScreenMode.allmode.CopyType.Disabled);
        }

        private void DropboxClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (DropboxClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                FtpClipboard.Checked = false;
                ImgurClipboard.Checked = false;
                SetClipboardType(ScreenMode.allmode.CopyType.Dropbox);
            }
            else
                SetClipboardType(ScreenMode.allmode.CopyType.Disabled);
        }

        private void ImgurClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (ImgurClipboard.Checked)
            {
                LocalClipboard.Checked = false;
                FtpClipboard.Checked = false;
                DropboxClipboard.Checked = false;
                SetClipboardType(ScreenMode.allmode.CopyType.ImgurAnon);
            }
            else
                SetClipboardType(ScreenMode.allmode.CopyType.Disabled);
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
        private void SetClipboardType(ScreenMode.allmode.CopyType Type)
        {
            Settings.Default.CopyLink = (int)Type;
            Settings.Default.Save();
        }
    }
}
