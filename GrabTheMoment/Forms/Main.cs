using System;
using System.Drawing;
using System.Windows.Forms;
#if __MonoCS__
using System.ComponentModel;
#endif
using GrabTheMoment.Properties;

namespace GrabTheMoment
{
    public partial class Main : Form
    {
#if __MonoCS__
        delegate void SetWindowStateCallback(FormWindowState State);
#endif
        public Icon YeahunterIcon { get; private set; }
        // Linux bug fix. Sajnos amikor megkapja a WindowState az új értéket akkor nem menti el ezért kell egy külön változó.
        // Szerencsére azért a minimalizálást elvégzi.
        public FormWindowState CopyWindowState { get; private set; }

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
            CopyWindowState = this.WindowState;
#if __MonoCS__
            this.FormClosed += Form1_FormClosed;
#endif
            // Középre helyezi az ablakot induláskor.
            this.StartPosition = FormStartPosition.CenterScreen;

            Log.LogPath = string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM"));
            ConfigLoad();
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

#if __MonoCS__
        // Külön szálon való futtathatóság miatt kell.
        public void SetWindowState(FormWindowState State)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetWindowStateCallback d = new SetWindowStateCallback(SetWindowState);
                this.Invoke(d, new object[] { State });
            }
            else
            {
                this.WindowState = State;
                CopyWindowState = State;

                if (State != FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = true;
#if !__MonoCS__
                    notifyIcon1.Visible = false;
#else
                    this.Activate();
#endif
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            InterceptKeys.UninitLinux();
            Gtk.Application.Quit();
            //Application.Exit();
        }
#endif

#if !__MonoCS__
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                SetWindowState(FormWindowState.Normal);
        }
#endif

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

#if !__MonoCS__
        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(Control.MousePosition);
        }
#endif

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.ShowInTaskbar = false;
#if !__MonoCS__
                notifyIcon1.Visible = true;
#endif
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
#if !__MonoCS__
        private void lastLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }
#endif

        // Beallitja a CopyLink erteket
        private void SetClipboardType(CopyType Type)
        {
            Settings.Default.CopyLink = (int)Type;
            Settings.Default.Save();
        }
    }
}
