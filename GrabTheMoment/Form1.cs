using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing.Imaging;
using GrabTheMoment.Properties;
using System.IO;
using System.Net;

namespace GrabTheMoment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkBox1.Checked = Settings.Default.MLocal;
            checkBox2.Checked = Settings.Default.MFtp;
            localToolStripMenuItem.Enabled = Settings.Default.MLocal;
            fTPToolStripMenuItem.Enabled = Settings.Default.MFtp;
        }
        //public void notifyIcon(int timeout, string tiptitle, string tiptext, ToolTipIcon tipicon)
        //{
        //    notifyIcon1.ShowBalloonTip(timeout, tiptitle, tiptext, tipicon);
        //}

        public void MLocal_SavePS(Bitmap bmpScreenShot, string neve)
        {
            bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + neve + ".png", ImageFormat.Png);
        }

        public void MFtp_SavePS(Bitmap bmpScreenShot, string neve)
        {
            neve = neve + ".png";
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + Settings.Default.MFtp_address + "/" + Settings.Default.MFtp_remotedir + "/" + neve);
            req.UseBinary = true;
            req.UsePassive = true;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(Settings.Default.MFtp_user, Settings.Default.MFtp_password);
            byte[] filedata = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                bmpScreenShot.Save(stream, ImageFormat.Png);
                stream.Close();

                filedata = stream.ToArray();
            }
            req.ContentLength = filedata.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(filedata, 0, filedata.Length);
            reqStream.Close();
            Clipboard.SetText(Settings.Default.MFtp_path + "/" + neve);
        }

        public void FullPS()
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenwidth, screenheight));
            if (Settings.Default.MLocal)
                MLocal_SavePS(bmpScreenShot, idodatum);
            if (Settings.Default.MFtp)
                MFtp_SavePS(bmpScreenShot, idodatum);
            notifyIcon1.ShowBalloonTip(5000, "FullPS", idodatum, ToolTipIcon.Info);
        }

        public void WindowPs(Rectangle rectangle)
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X;
            int ycoord = rectangle.Y;
            int windowwidth = rectangle.Width - xcoord;
            int windowheight = rectangle.Height - ycoord;
            if (xcoord == -8 && ycoord == -8)
            {
                xcoord += 8;
                ycoord += 8;
                windowwidth -= 16;
                windowheight -= 16;
            }
            Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(xcoord, ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            if (Settings.Default.MLocal)
                MLocal_SavePS(bmpScreenShot, idodatum);
            if (Settings.Default.MFtp)
                MFtp_SavePS(bmpScreenShot, idodatum);
            notifyIcon1.ShowBalloonTip(5000, "WindowPs", idodatum, ToolTipIcon.Info);
        }

        public void AreaPs(Rectangle rectangle)
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X;
            int ycoord = rectangle.Y;
            int windowwidth = rectangle.Width - xcoord;
            int windowheight = rectangle.Height - ycoord;
            if (xcoord == -8 && ycoord == -8)
            {
                xcoord += 8;
                ycoord += 8;
                windowwidth -= 16;
                windowheight -= 16;
            }
            Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(xcoord, ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + idodatum + ".png", ImageFormat.Png);
            notifyIcon1.ShowBalloonTip(5000, "WindowPs", idodatum, ToolTipIcon.Info);
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
                //this.Hide();
                //this.WindowState = FormWindowState.Normal;
            }
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.local localForm = new Savemode.local();
            localForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MLocal = checkBox1.Checked;
            localToolStripMenuItem.Enabled = Settings.Default.MLocal;
            Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MFtp = checkBox2.Checked;
            fTPToolStripMenuItem.Enabled = Settings.Default.MFtp;
            Settings.Default.Save();
        }

        private void fTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.FTP ftpForm = new Savemode.FTP();
            ftpForm.Show();
        }
    }
}
