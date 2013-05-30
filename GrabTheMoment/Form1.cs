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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;

namespace GrabTheMoment
{
    public partial class Form1 : Form
    {
        Log log = new Log();
        public Form1()
        {
            InitializeComponent();
            checkBox5.Enabled = checkBox1.Checked = Settings.Default.MLocal;
            checkBox6.Enabled = checkBox2.Checked = Settings.Default.MFtp;
            checkBox3.Checked = Settings.Default.MDropbox;
            checkBox8.Enabled = checkBox4.Checked = Settings.Default.MImgur;
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


            switch (Settings.Default.CopyLink)
            {
                case 1:
                    checkBox5.Checked = true;
                    break;
                case 2:
                    checkBox6.Checked = true;
                    break;
                case 3:
                    
                    break;
                case 4:
                    checkBox8.Checked = true;
                    break;
            }

        }

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
            Savemode.Forms.Dropbox dropboxForm = new Savemode.Forms.Dropbox();
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

        private void lastLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            InterceptKeys.Klipbood();
        }
    }
}
