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
    public partial class local : Form
    {
        public local()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.SaveLocation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Settings.Default.SaveLocation = textBox1.Text;
                Settings.Default.Save();
            }
        }
    }
}
