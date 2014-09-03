﻿using System;
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
        delegate void SetVisibleCallback(bool Visible);
#endif
        public Icon YeahunterIcon { get; private set; }

        public Main()
        {
            InitializeComponent();
#if __MonoCS__
            this.FormClosed += Form1_FormClosed;
#endif
            // Középre helyezi az ablakot induláskor.
            this.StartPosition = FormStartPosition.CenterScreen;

            Log.LogPath = string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM"));
            localToolStripMenuItem.Enabled      = checkBox5.Enabled = checkBox1.Checked = Settings.Default.MLocal;
            fTPToolStripMenuItem.Enabled        = checkBox6.Enabled = checkBox2.Checked = Settings.Default.MFtp;
            dropboxToolStripMenuItem.Enabled    = checkBox7.Enabled = checkBox3.Checked = Settings.Default.MDropbox;
            checkBox8.Enabled = checkBox4.Checked = Settings.Default.MImgur;

            switch (Settings.Default.CopyLink)
            {
                case 1:
                    checkBox5.Checked = true;
                    break;
                case 2:
                    checkBox6.Checked = true;
                    break;
                case 3:
                    checkBox7.Checked = true;
                    break;
                case 4:
                    checkBox8.Checked = true;
                    break;
            }
        }

#if __MonoCS__
        // Külön szálon való futtathatóság miatt kell.
        public void SetVisible(bool Visible)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                this.Invoke(d, new object[] { Visible });
            }
            else
            {
                this.Visible = Visible;
                // Bug fix.
                if(Visible)
                    this.Activate();
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
                //this.Hide();
                //this.WindowState = FormWindowState.Normal;
            }
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.local localForm = new Savemode.Forms.local();
            localForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MLocal = checkBox5.Enabled = localToolStripMenuItem.Enabled = checkBox1.Checked;
            if (!checkBox5.Enabled)
                checkBox5.Checked = false;
            Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MFtp = checkBox6.Enabled = fTPToolStripMenuItem.Enabled = checkBox2.Checked;
            if (!checkBox6.Enabled)
                checkBox6.Checked = false;
            Settings.Default.Save();
        }

        private void fTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.FTP ftpForm = new Savemode.Forms.FTP();
            ftpForm.Show();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MDropbox = checkBox7.Enabled = dropboxToolStripMenuItem.Enabled = checkBox3.Checked;
            if (!checkBox7.Enabled)
                checkBox7.Checked = false;
            Settings.Default.Save();
        }

        private void dropboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Forms.Dropbox dropboxForm = new Savemode.Forms.Dropbox();
            dropboxForm.Show();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MImgur = checkBox8.Enabled = checkBox4.Checked;
            if (!checkBox8.Enabled)
                checkBox8.Checked = false;
            Settings.Default.Save();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                Settings.Default.CopyLink = 1;
            }
            else
                Settings.Default.CopyLink = 0;
            Settings.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                Settings.Default.CopyLink = 2;
            }
            else
                Settings.Default.CopyLink = 0;
            Settings.Default.Save();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox8.Checked = false;
                Settings.Default.CopyLink = 3;
            }
            else
                Settings.Default.CopyLink = 0;
            Settings.Default.Save();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                Settings.Default.CopyLink = 4;
            }
            else
                Settings.Default.CopyLink = 0;
            Settings.Default.Save();
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
    }
}
