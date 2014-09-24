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
            Path.Text = Settings.Default.MLocal_path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                Path.Text = FolderBrowser.SelectedPath;
                Settings.Default.MLocal_path = Path.Text;
                Settings.Default.Save();
            }
        }
    }
}
