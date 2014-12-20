using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Forms.Savemode
{
    public partial class FTP : Form
    {
        public FTP()
        {
            InitializeComponent();
            FtpUser.Text = Settings.Default.MFtp_user;
            FtpPassword.Text = Settings.Default.MFtp_password;
            FtpServer.Text = Settings.Default.MFtp_address;
            FtpDirectory.Text = Settings.Default.MFtp_remotedir;
            Url.Text = Settings.Default.MFtp_path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.MFtp_user = FtpUser.Text;
            Settings.Default.MFtp_password = FtpPassword.Text;
            Settings.Default.MFtp_address = FtpServer.Text;
            Settings.Default.MFtp_remotedir = FtpDirectory.Text;
            Settings.Default.MFtp_path = Url.Text;
            Settings.Default.Save();
            Save.Enabled = false;
            Save.Text = "Mentve!";
        }
    }
}
