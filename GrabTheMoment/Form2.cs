using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using GrabTheMoment.Windows;

namespace GrabTheMoment
{
    public partial class Form2 : Form
    {
        private Graphics formGraphics;
        private bool isDown = false;
        private int initialX;
        private int initialY;
        private Rectangle rect;

        public Form2()
        {
            InitializeComponent();
            Screenmode.allmode.mekkoraazxesazy();
            WinApi.SetWinFullScreen(this.Handle, Screenmode.allmode.x, Screenmode.allmode.y);
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
            new Thread(() => Screenmode.allmode.AreaPs(rect)).Start();
            this.Close();
        }
    }
}
