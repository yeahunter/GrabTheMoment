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

namespace GrabTheMoment.Savemode
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

        private void FTP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.MFtp_user = textBox1.Text;
            Settings.Default.MFtp_password = textBox2.Text;
            Settings.Default.MFtp_address = textBox3.Text;
            Settings.Default.MFtp_remotedir = textBox4.Text;
            Settings.Default.MFtp_path = textBox5.Text;
            Settings.Default.Save();
        }
    }
}
