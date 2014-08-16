using System;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode.Forms
{
    public partial class local : Form
    {
        public local()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.MLocal_path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Settings.Default.MLocal_path = textBox1.Text;
                Settings.Default.Save();
            }
        }
    }
}
