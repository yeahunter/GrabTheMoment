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
        public static string holadropbox = "";
        public Dropbox()
        {
            InitializeComponent();
            var dbPath = System.IO.Path.Combine(
         Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dropbox\\host.db");
            string[] lines = System.IO.File.ReadAllLines(dbPath);
            byte[] dbBase64Text = Convert.FromBase64String(lines[1]);
            holadropbox = System.Text.ASCIIEncoding.ASCII.GetString(dbBase64Text);
            if (Settings.Default.MDropbox_path == "")
            {
                Settings.Default.MDropbox_path = holadropbox;
                Settings.Default.Save();
            }
            textBox1.Text = Settings.Default.MDropbox_path;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = holadropbox;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Settings.Default.MDropbox_path = textBox1.Text;
                Settings.Default.Save();
            }
        }
    }
}
