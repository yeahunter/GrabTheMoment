using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GrabTheMoment
{
    public partial class Form2 : Form
    {
        System.Drawing.Graphics formGraphics;
        bool isDown = false;
        int initialX;
        int initialY;

        public Form2()
        {
            InitializeComponent();
            WinApi.SetWinFullScreen(this.Handle);
            this.Activate();
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = System.Drawing.Color.Transparent;
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
                Rectangle rect = new Rectangle(Math.Min(e.X, initialX),
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

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }
    }
}
