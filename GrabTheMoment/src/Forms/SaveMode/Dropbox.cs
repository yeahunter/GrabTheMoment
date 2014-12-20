using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode.Forms
{
    public partial class Dropbox : Form
    {

        private string reqtoken = null;
        private string reqsecret = null;
        private string eventecske = null;

        public Dropbox()
        {
            InitializeComponent();
            UploadLabel.Checked = Settings.Default.MDropbox_upload;
        }

        private void RequestAccessButton_Click(object sender, EventArgs e)
        {
            API.Dropbox_oauth1.requestToken(ref reqtoken, ref reqsecret);
            API.Dropbox_oauth1.Authorize(reqtoken);
            RequestAccessButton.Enabled = false;
        }

        private void CheckAccessButton_Click(object sender, EventArgs e)
        {
            API.Dropbox_oauth1.AccessToken(reqtoken, reqsecret, ref eventecske);
            CheckAccessButton.Visible = CheckAccessButton.Enabled = false;
            ReceivedData.Text = eventecske;
            ReceivedData.Visible = true;
            if (eventecske == "OK")
            {
                UploadLabel.Enabled = true;
            }
        }

        private void Dropbox_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MDropbox_upload = UploadLabel.Checked;
            Settings.Default.Save();
        }
    }
}
