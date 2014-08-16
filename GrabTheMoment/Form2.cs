using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GrabTheMoment
{
    public partial class Form2 : Form
    {
        private Screenmode.allmode smode;
        private System.Drawing.Graphics formGraphics;
        private bool isDown = false;
        private int initialX;
        private int initialY;
        private Rectangle rect;

        public Form2()
        {
            InitializeComponent();
            smode = InterceptKeys.smode;
            smode.mekkoraazxesazy();
            WinApi.SetWinFullScreen(this.Handle, smode.x, smode.y);
            this.Activate();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Invalidate();
                this.Update();
                //this.Invalidate();
                //this.Refresh();
                float penwidth = 1;
                Pen drwaPen = new Pen(ForeColor, penwidth);
                SolidBrush brush = new SolidBrush(Color.Red);
                //Pen backPen = new Pen(Color.Red, penwidth);
                int width = e.X - initialX;
                int height = e.Y - initialY;
                rect = new Rectangle(Math.Min(e.X, initialX),
                               Math.Min(e.Y, initialY),
                               Math.Abs(width),
                               Math.Abs(height));

                
                Rectangle inner = rect;
                inner.X += (int)penwidth;
                inner.Y += (int)penwidth;
                inner.Width -= (int)penwidth;
                inner.Height -= (int)penwidth;
                formGraphics = this.CreateGraphics();
                formGraphics.DrawRectangle(drwaPen, rect);
                formGraphics.FillRectangle(brush, inner);
                //formGraphics.DrawRectangle(backPen, inner);
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            new System.Threading.Thread(() => smode.AreaPs(rect)).Start();
            this.Close();
        }
    }
    public class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void
            SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                         int X, int Y, int width, int height, uint flags);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0x0040

        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN); }
        }

        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN); }
        }

        public static void SetWinFullScreen(IntPtr hwnd, int x, int y)
        {
            SetWindowPos(hwnd, HWND_TOP, x, y, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height, SWP_SHOWWINDOW);
        }
    }
}
