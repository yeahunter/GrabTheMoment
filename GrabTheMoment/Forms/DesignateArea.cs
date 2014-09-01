using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GrabTheMoment
{
    public partial class DesignateArea : Form
    {
        private Graphics formGraphics;
        private bool isDown = false;
        private int initialX;
        private int initialY;
        private Rectangle rect;

        public DesignateArea()
        {
            InitializeComponent();
            ScreenMode.allmode.mekkoraazxesazy();
            API.NativeWin32.SetWinFullScreen(this.Handle, ScreenMode.allmode.x, ScreenMode.allmode.y);
            this.Activate();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close(); // Barmelyik gomb lenyomasakor el fog tunni ez a form.
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
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
            new Thread(() => new ScreenMode.RectangleArea(rect)).Start();
            this.Close();
        }
    }
}
