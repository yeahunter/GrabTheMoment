﻿using System;
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

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PrintScreen)
                FullPS();
            else
            {
                notifyIcon1.ShowBalloonTip(5000, "Welcome", "Hello Peti", ToolTipIcon.Info);
            }

        }

        private void FullPS()
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenwidth, screenheight));
            bmpScreenShot.Save(idodatum + ".png", ImageFormat.Png);
            notifyIcon1.ShowBalloonTip(5000, "Screenshot készült", idodatum, ToolTipIcon.Info);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //contextMenuStrip1.Show(Control.MousePosition);
        }

        private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(Control.MousePosition);
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
