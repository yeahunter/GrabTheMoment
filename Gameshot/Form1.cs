using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Gameshot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DllImports.OdaIrjak(label1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // label1.Text = DllImports.GetActiveWindowTitle();
            DllImports.HookStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DllImports.HookStop();
        }
    }
}
