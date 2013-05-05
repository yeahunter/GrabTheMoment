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
using System.Web;
using System.Diagnostics;

namespace GrabTheMoment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkBox1.Checked = Settings.Default.MLocal;
            checkBox2.Checked = Settings.Default.MFtp;
            checkBox3.Checked = Settings.Default.MDropbox;
            localToolStripMenuItem.Enabled = Settings.Default.MLocal;
            fTPToolStripMenuItem.Enabled = Settings.Default.MFtp;

            var dbPath = System.IO.Path.Combine(
         Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\\host.db");
            if (File.Exists(dbPath))
            {
                checkBox3.Enabled = true;
            }
            else
            {
                checkBox3.Text += " (Nincs)";
                checkBox3.Enabled = false;
                checkBox3.Checked = false;
                if (Settings.Default.MDropbox)
                {
                    Settings.Default.MDropbox = false;
                    Settings.Default.Save();
                }
            }
        }
        //public void notifyIcon(int timeout, string tiptitle, string tiptext, ToolTipIcon tipicon)
        //{
        //    notifyIcon1.ShowBalloonTip(timeout, tiptitle, tiptext, tipicon);
        //}

        public void apitoken()
        {
            var consumerKey = "";
            var consumerSecret = "";
            var uri = new Uri("https://api.dropbox.com/1/oauth/request_token");

            // Generate a signature
            API.OAuth.OAuthBase oAuth = new API.OAuth.OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string parameters;
            string normalizedUrl;
            string signature = oAuth.GenerateSignature(uri, consumerKey, consumerSecret,
                String.Empty, String.Empty, "GET", timeStamp, nonce, API.OAuth.OAuthBase.SignatureTypes.HMACSHA1,
                out normalizedUrl, out parameters);

            signature = HttpUtility.UrlEncode(signature);

            StringBuilder requestUri = new StringBuilder(uri.ToString());
            requestUri.AppendFormat("?oauth_consumer_key={0}&", consumerKey);
            requestUri.AppendFormat("oauth_nonce={0}&", nonce);
            requestUri.AppendFormat("oauth_timestamp={0}&", timeStamp);
            requestUri.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            requestUri.AppendFormat("oauth_version={0}&", "1.0");
            requestUri.AppendFormat("oauth_signature={0}", signature);

            var request = (HttpWebRequest)WebRequest.Create(new Uri(requestUri.ToString()));
            request.Method = WebRequestMethods.Http.Get;

            var response = request.GetResponse();

            var queryString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var parts = queryString.Split('&');
            var token = parts[1].Substring(parts[1].IndexOf('=') + 1);
            var tokenSecret = parts[0].Substring(parts[0].IndexOf('=') + 1);

            Settings.Default.oauth_token = token;
            Settings.Default.oauth_token_secret = tokenSecret;
            Settings.Default.Save();
        }


        public void MLocal_SavePS(Bitmap bmpScreenShot, string neve)
        {
            bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + neve + ".png", ImageFormat.Png);
        }

        public void MDropbox_SavePS(Bitmap bmpScreenShot, string neve)
        {
            if (!File.Exists(Settings.Default.MDropbox_path))
                System.IO.Directory.CreateDirectory(Settings.Default.MDropbox_path);
            bmpScreenShot.Save(Settings.Default.MDropbox_path + "\\" + neve + ".png", ImageFormat.Png);
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
            //if (Settings.Default.MDropbox)
            //    MDropbox_SavePS(bmpScreenShot, idodatum);
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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MDropbox = checkBox3.Checked;

            var dbPath = System.IO.Path.Combine(
Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\\host.db");
            string[] lines = System.IO.File.ReadAllLines(dbPath);
            byte[] dbBase64Text = Convert.FromBase64String(lines[1]);
            string holadropbox = System.Text.ASCIIEncoding.ASCII.GetString(dbBase64Text);
            string pathString = System.IO.Path.Combine(holadropbox, "GrabTheMoment");
            if (!File.Exists(pathString))
                System.IO.Directory.CreateDirectory(pathString);
            if (Settings.Default.MDropbox_path == "")
            {
                Settings.Default.MDropbox_path = pathString;
                Settings.Default.Save();
            }

            //dropboxToolStripMenuItem.Enabled = Settings.Default.MDropbox;
            //Settings.Default.Save();
        }

        private void dropboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Savemode.Dropbox dropboxForm = new Savemode.Dropbox();
            dropboxForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //apitoken();
            label1.Text = Settings.Default.oauth_token;
            var queryString = String.Format("oauth_token={0}", Settings.Default.oauth_token);
            var authorizeUrl = "https://www.dropbox.com/1/oauth/authorize?" + queryString;
            Process.Start(authorizeUrl);
        }
    }
}
