using System;
using System.Drawing;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Savemode.Forms
{
    public partial class local : Form
    {
        //Form1 fone = null;
        public local()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.MLocal_path;
            //fone = new Form1();
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

        public void FullPS()
        {
            //string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            //int screenheight = Screen.PrimaryScreen.Bounds.Height;
            //int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            //Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
            //Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            //gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenwidth, screenheight));
            //bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + idodatum + ".png", ImageFormat.Png);
            //fone.notifyIcon(5000, "FullPS", idodatum, ToolTipIcon.Info);
        }

        public void WindowPs(Rectangle rectangle)
        {
            //string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            //int xcoord = rectangle.X;
            //int ycoord = rectangle.Y;
            //int windowwidth = rectangle.Width - xcoord;
            //int windowheight = rectangle.Height - ycoord;
            //if (xcoord == -8 && ycoord == -8)
            //{
            //    xcoord += 8;
            //    ycoord += 8;
            //    windowwidth -= 16;
            //    windowheight -= 16;
            //}
            //Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            //Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            //gfx.CopyFromScreen(xcoord, ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            //bmpScreenShot.Save(Settings.Default.MLocal_path + "\\" + idodatum + ".png", ImageFormat.Png);
            //fone.notifyIcon(5000, "WindowPs", idodatum, ToolTipIcon.Info);
        }
    }
}
