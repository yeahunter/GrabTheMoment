using System;
using System.Drawing;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Screenmode
{
    class allmode
    {
        private Savemode.allmode savemode = new Savemode.allmode();
        public int x, y;

        public string WhatClipboard()
        {
            string visszater = string.Empty;

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

        public void mekkoraazxesazy()
        {
            foreach (var asztal in Screen.AllScreens)
            {
                int iksz = asztal.Bounds.X;
                if (asztal.Bounds.X < x)
                    x = iksz;
                int ipszilon = asztal.Bounds.Y;
                if (ipszilon < y)
                    y = ipszilon;
            }
        }

        public void DrawWatermark(Graphics gfx)
        {
            int mekkorabetuk = (int)(Math.Pow(gfx.VisibleClipBounds.Width * gfx.VisibleClipBounds.Height, (1.0 / 3.3)));
            var font = new Font("Arial", mekkorabetuk, FontStyle.Bold, GraphicsUnit.Pixel);
            var color = Color.FromArgb(25, 127, 127, 127);
            var brush = new SolidBrush(color);

            string theString = "gtm.peti.info";
            var sz = gfx.VisibleClipBounds.Size;
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
            var fone = InterceptKeys.windowsformoscucc;
            fone.notifyIcon1.ShowBalloonTip(timeout, tiptitle, tiptext + " (Kattints ide, hogy a vágólapra kerüljön a link)", tipicon);
        }

        public void FullPS()
        {
            try
            {
                string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
                int screenheight = SystemInformation.VirtualScreen.Height;
                int screenwidth = SystemInformation.VirtualScreen.Width;

                mekkoraazxesazy();

                var bmpScreenShot = new Bitmap(screenwidth, screenheight);
                var gfx = Graphics.FromImage((Image)bmpScreenShot);
                gfx.CopyFromScreen(x, y, 0, 0, new Size(screenwidth, screenheight));

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

                if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                    savemode.MDropbox_SavePS(bmpScreenShot, idodatum);

                notifyIcon(7000, "FullPS" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
                Log.WriteEvent("Form1/FullPS: " + idodatum + " elkészült!");
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/FullPS: ", e);
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

            var bmpScreenShot = new Bitmap(windowwidth, windowheight);
            var gfx = Graphics.FromImage((Image)bmpScreenShot);
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

            if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                savemode.MDropbox_SavePS(bmpScreenShot, idodatum);

            notifyIcon(7000, "WindowPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            Log.WriteEvent("Form1/WindowPs: Settings.Default.CopyLink: " + Settings.Default.CopyLink);
            Log.WriteEvent("Form1/WindowPs: " + idodatum + " elkészült!");
        }

        public void AreaPs(Rectangle rectangle)
        {
            Log.WriteEvent("Form1/AreaPs: Meghivodtam!");
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X + 1, ycoord = rectangle.Y + 1, windowwidth = rectangle.Width -1, windowheight = rectangle.Height - 1;
            var bmpScreenShot = new Bitmap(windowwidth, windowheight);
            var gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(x + xcoord, y + ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            DrawWatermark(gfx);

            if (Settings.Default.MLocal)
                savemode.MLocal_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MFtp)
                savemode.MFtp_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MImgur)
                savemode.MImgur_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                savemode.MDropbox_SavePS(bmpScreenShot, idodatum);

            notifyIcon(5000, "AreaPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            Log.WriteEvent("Form1/AreaPs: " + idodatum + " elkészült!");
        }
    }
}
