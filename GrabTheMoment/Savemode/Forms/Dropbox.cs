using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode.Forms
{
    public partial class Dropbox : Form
    {

        string reqtoken = null, reqsecret = null, eventecske = null;
        public Dropbox()
        {
            InitializeComponent();
            checkBox1.Checked = Settings.Default.MDropbox_upload;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            API.Dropbox_oauth1.requestToken(ref reqtoken, ref reqsecret);
            API.Dropbox_oauth1.Authorize(reqtoken);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            API.Dropbox_oauth1.AccessToken(reqtoken, reqsecret, ref eventecske);
            label2.Text = eventecske;
            if (eventecske == "OK")
            {
                checkBox1.Enabled = true;
            }
        }

        private void Dropbox_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MDropbox_upload = checkBox1.Checked;
            Settings.Default.Save();
        }
    }
}
