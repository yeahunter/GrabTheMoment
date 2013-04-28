using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing.Imaging;

namespace GrabTheMoment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenwidth, screenheight));
            bmpScreenShot.Save(DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss") + ".png", ImageFormat.Png);
            //this.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
        }
    }
}
