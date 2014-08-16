using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode.Forms
{
    public partial class FTP : Form
    {
        public FTP()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.MFtp_user;
            textBox2.Text = Settings.Default.MFtp_password;
            textBox3.Text = Settings.Default.MFtp_address;
            textBox4.Text = Settings.Default.MFtp_remotedir;
            textBox5.Text = Settings.Default.MFtp_path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.MFtp_user = textBox1.Text;
            Settings.Default.MFtp_password = textBox2.Text;
            Settings.Default.MFtp_address = textBox3.Text;
            Settings.Default.MFtp_remotedir = textBox4.Text;
            Settings.Default.MFtp_path = textBox5.Text;
            Settings.Default.Save();
            button1.Enabled = false;
            button1.Text = "Mentve!";
        }
    }
}
