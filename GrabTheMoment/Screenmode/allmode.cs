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
using GrabTheMoment.Properties;
using System.IO;
using System.Net;
using System.Web;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;

namespace GrabTheMoment.Screenmode
{
    class allmode
    {
        Log log = new Log();
        Savemode.allmode savemode = new Savemode.allmode();
        public string WhatClipboard()
        {
            string visszater = "";
            switch (Settings.Default.CopyLink)
            {
                case 0:
                    visszater = "NincsCopy";
                    break;
                case 1:
                    visszater = "LocalCopy";
                    break;
                case 2:
                    visszater = "FTPCopy";
                    break;
                case 3:
                    visszater = "DropboxCopy";
                    break;
                case 4:
                    visszater = "ImgurCopy";
                    break;
                default:
                    visszater = "???Copy";
                    break;
            }
            return visszater;
        }

        public void DrawWatermark(Graphics gfx)
        {
            int mekkorabetuk = (int)(Math.Pow(gfx.VisibleClipBounds.Width * gfx.VisibleClipBounds.Height, (1.0 / 3.3)));
            Font font = new Font("Arial", mekkorabetuk, FontStyle.Bold, GraphicsUnit.Pixel);
            Color color = Color.FromArgb(50, 127, 127, 127);
            SolidBrush brush = new SolidBrush(color);

            String theString = "gtm.peti.info";
            SizeF sz = gfx.VisibleClipBounds.Size;
            gfx.TranslateTransform(sz.Width / 2, sz.Height / 2);
            float fok = -45;
            fok *= sz.Height / sz.Width;

            gfx.RotateTransform(fok);
            sz = gfx.MeasureString(theString, font);
            //Offset the Drawstring method so that the center of the string matches the center.
            gfx.DrawString(theString, font, brush, -(sz.Width / 2), -(sz.Height / 2));
            //Reset the graphics object Transformations.
            gfx.ResetTransform();
        }

        public void notifyIcon(int timeout, string tiptitle, string tiptext, ToolTipIcon tipicon)
        {
            Form1 fone = InterceptKeys.windowsformoscucc;
            fone.notifyIcon1.ShowBalloonTip(timeout, tiptitle, tiptext + " (Kattints ide, hogy a vágólapra kerüljön a link)", tipicon);
        }

        public void FullPS()
        {
            try
            {
                string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
                int screenheight = Screen.PrimaryScreen.Bounds.Height;
                int screenwidth = Screen.PrimaryScreen.Bounds.Width;
                Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
                Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
                gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenwidth, screenheight));

                DrawWatermark(gfx);

                if (Settings.Default.MLocal)
                    savemode.MLocal_SavePS(bmpScreenShot, idodatum);
                if (Settings.Default.MFtp)
                {
                    //System.Threading.Thread.Sleep(5000);
                    savemode.MFtp_SavePS(bmpScreenShot, idodatum);
                }
                //if (Settings.Default.MDropbox)
                //    MDropbox_SavePS(bmpScreenShot, idodatum);
                if (Settings.Default.MImgur)
                    savemode.MImgur_SavePS(bmpScreenShot, idodatum);
                notifyIcon(7000, "FullPS" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
                log.WriteEvent("Form1/FullPS: " + idodatum + " elkészült!");
            }
            catch (Exception e)
            {
                log.WriteExceptionEvent(e, "Form1/FullPS: ");
            }
        }

        public void WindowPs(Rectangle rectangle)
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X;
            int ycoord = rectangle.Y;
            int windowwidth = rectangle.Width - xcoord;
            int windowheight = rectangle.Height - ycoord;
            if (xcoord == -8 && ycoord == -8)
            {
                xcoord += 8;
                ycoord += 8;
                windowwidth -= 16;
                windowheight -= 16;
            }
            Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(xcoord, ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);

            DrawWatermark(gfx);

            if (Settings.Default.MLocal)
                savemode.MLocal_SavePS(bmpScreenShot, idodatum);
            if (Settings.Default.MFtp)
            {
                //System.Threading.Thread.Sleep(5000);
                savemode.MFtp_SavePS(bmpScreenShot, idodatum);
            }
            if (Settings.Default.MImgur)
                savemode.MImgur_SavePS(bmpScreenShot, idodatum);
            notifyIcon(7000, "WindowPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            log.WriteEvent("Form1/WindowPs: Settings.Default.CopyLink: " + Settings.Default.CopyLink);
            log.WriteEvent("Form1/WindowPs: " + idodatum + " elkészült!");
        }

        public void AreaPs(Rectangle rectangle)
        {
            log.WriteEvent("Form1/AreaPs: Meghivodtam!");
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X + 1, ycoord = rectangle.Y + 1, windowwidth = rectangle.Width -1, windowheight = rectangle.Height - 1;
            Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(xcoord, ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            DrawWatermark(gfx);
            if (Settings.Default.MLocal)
                savemode.MLocal_SavePS(bmpScreenShot, idodatum);
            if (Settings.Default.MFtp)
                savemode.MFtp_SavePS(bmpScreenShot, idodatum);
            if (Settings.Default.MImgur)
                savemode.MImgur_SavePS(bmpScreenShot, idodatum);
            notifyIcon(5000, "AreaPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            log.WriteEvent("Form1/AreaPs: " + idodatum + " elkészült!");
        }
    }
}
