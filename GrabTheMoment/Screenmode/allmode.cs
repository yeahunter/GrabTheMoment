using System;
using System.Drawing;
using System.Windows.Forms;
using GrabTheMoment.Properties;
#if __MonoCS__
using Notifications;
#endif

namespace GrabTheMoment.Screenmode
{
    static class allmode
    {
        public static int x, y;

        private static string WhatClipboard()
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

        public static void mekkoraazxesazy()
        {
            foreach (Screen asztal in Screen.AllScreens)
            {
                int iksz = asztal.Bounds.X;
                if (asztal.Bounds.X < x)
                    x = iksz;
                int ipszilon = asztal.Bounds.Y;
                if (ipszilon < y)
                    y = ipszilon;
            }
        }

        private static void DrawWatermark(Graphics gfx)
        {
            int mekkorabetuk = (int)(Math.Pow(gfx.VisibleClipBounds.Width * gfx.VisibleClipBounds.Height, (1.0 / 3.3)));
            Font font = new Font("Arial", mekkorabetuk, FontStyle.Bold, GraphicsUnit.Pixel);
            Color color = Color.FromArgb(25, 127, 127, 127);
            SolidBrush brush = new SolidBrush(color);

            string theString = "gtm.peti.info";
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

        private static void notifyIcon(int timeout, string tiptitle, string tiptext, ToolTipIcon tipicon)
        {
#if !__MonoCS__
            Form1 fone = InterceptKeys.windowsformoscucc;
            fone.notifyIcon1.ShowBalloonTip(timeout, tiptitle, tiptext + " (Kattints ide, hogy a vágólapra kerüljön a link)", tipicon);
#else
            Notification n = new Notification(tiptitle, tiptext);
            n.AddHint("x-canonical-append", "");
            n.Timeout = 15;
            n.Show();
#endif
        }

        public static void FullPS()
        {
            try
            {
                string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
                int screenheight = SystemInformation.VirtualScreen.Height;
                int screenwidth = SystemInformation.VirtualScreen.Width;

                mekkoraazxesazy();

                Bitmap bmpScreenShot = new Bitmap(screenwidth, screenheight);
                Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
                gfx.CopyFromScreen(x, y, 0, 0, new Size(screenwidth, screenheight));

                DrawWatermark(gfx);

                if (Settings.Default.MLocal)
                    Savemode.allmode.MLocal_SavePS(bmpScreenShot, idodatum);

                if (Settings.Default.MFtp)
                {
                    //System.Threading.Thread.Sleep(5000);
                    Savemode.allmode.MFtp_SavePS(bmpScreenShot, idodatum);
                }

                //if (Settings.Default.MDropbox)
                //    MDropbox_SavePS(bmpScreenShot, idodatum);

                if (Settings.Default.MImgur)
                    Savemode.allmode.MImgur_SavePS(bmpScreenShot, idodatum);

                if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                    Savemode.allmode.MDropbox_SavePS(bmpScreenShot, idodatum);

                notifyIcon(7000, "FullPS" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
                Log.WriteEvent("Form1/FullPS: " + idodatum + " elkészült!");
            }
            catch (Exception e)
            {
                Log.WriteEvent("Form1/FullPS: ", e);
            }
        }

        public static void WindowPs(Rectangle rectangle)
        {
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X;
            int ycoord = rectangle.Y;
#if !__MonoCS__
            int windowwidth = rectangle.Width - xcoord;
            int windowheight = rectangle.Height - ycoord;
#else
            int windowwidth = rectangle.Width;
            int windowheight = rectangle.Height;
#endif

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
                Savemode.allmode.MLocal_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MFtp)
            {
                //System.Threading.Thread.Sleep(5000);
                Savemode.allmode.MFtp_SavePS(bmpScreenShot, idodatum);
            }

            if (Settings.Default.MImgur)
                Savemode.allmode.MImgur_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                Savemode.allmode.MDropbox_SavePS(bmpScreenShot, idodatum);

            notifyIcon(7000, "WindowPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            Log.WriteEvent("Form1/WindowPs: Settings.Default.CopyLink: " + Settings.Default.CopyLink);
            Log.WriteEvent("Form1/WindowPs: " + idodatum + " elkészült!");
        }

        public static void AreaPs(Rectangle rectangle)
        {
            Log.WriteEvent("Form1/AreaPs: Meghivodtam!");
            string idodatum = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
            int xcoord = rectangle.X + 1, ycoord = rectangle.Y + 1, windowwidth = rectangle.Width -1, windowheight = rectangle.Height - 1;
            Bitmap bmpScreenShot = new Bitmap(windowwidth, windowheight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(x + xcoord, y + ycoord, 0, 0, new Size(windowwidth, windowheight), CopyPixelOperation.SourceCopy);
            DrawWatermark(gfx);

            if (Settings.Default.MLocal)
                Savemode.allmode.MLocal_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MFtp)
                Savemode.allmode.MFtp_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MImgur)
                Savemode.allmode.MImgur_SavePS(bmpScreenShot, idodatum);

            if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                Savemode.allmode.MDropbox_SavePS(bmpScreenShot, idodatum);

            notifyIcon(5000, "AreaPs" + " + " + WhatClipboard(), idodatum, ToolTipIcon.Info);
            Log.WriteEvent("Form1/AreaPs: " + idodatum + " elkészült!");
        }
    }
}
