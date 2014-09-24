using GrabTheMoment.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrabTheMoment.ScreenMode
{
    public abstract class PrintScreenType
    {
        private string _FileName;
        private string _PrintScreenType;

        private int _Height, _Width, _X, _Y;

        private Graphics _Gfx;
        private Bitmap _bmpScreenShot;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected void SetFileName()
        {
            _FileName = DateTime.Now.ToString("yyyy.MM.dd.-HH.mm.ss");
        }

        public int X
        {
            get { return _X; }
            set { _X = value; }
        }

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public string Type
        {
            get { return _PrintScreenType; }
            set { _PrintScreenType = value; }
        }

        protected void SetXandY()
        {
            foreach (Screen Kijelzo in Screen.AllScreens)
            {
                int iksz = Kijelzo.Bounds.X;
                if (Kijelzo.Bounds.X < _X)
                    _X = iksz;
                int ipszilon = Kijelzo.Bounds.Y;
                if (ipszilon < _Y)
                    _Y = ipszilon;
            }
        }

        protected void CreatePic()
        {
            _bmpScreenShot = new Bitmap(_Width, _Height);
            _Gfx = Graphics.FromImage((Image)_bmpScreenShot);
            _Gfx.CopyFromScreen(_X, _Y, 0, 0, new Size(_Width, _Height));
            Log.WriteEvent(String.Format("x:{0} y:{1} w:{2} h:{3}", _X, _Y, _Width, _Height));
            DrawWatermark(_Gfx);
        }

        private void DrawWatermark(Graphics gfx)
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

        protected void notifyIcon(string ScreenMode, string tiptext)
        {
            Main fone = InterceptKeys.windowsformoscucc;
            int Timeout = 7 * 1000; // Ennyi masodpercig mutassa az uzenetet
            string Title = String.Format("{0} + {1}", ScreenMode, Main.WhatClipboard());
            string Text = string.Format("{0} (Kattints ide, hogy a vágólapra kerüljön a link)", tiptext);

            fone.notifyIcon1.ShowBalloonTip(Timeout, Title, Text, ToolTipIcon.Info);
        }

        protected void SavePic()
        {
            if (Settings.Default.MLocal)
                SaveMode.Local(_bmpScreenShot, _FileName);

            if (Settings.Default.MFtp)
                SaveMode.FTP(_bmpScreenShot, _FileName);

            if (Settings.Default.MImgur)
                SaveMode.ImgurAnon(_bmpScreenShot, _FileName);

            if (Settings.Default.MDropbox && Settings.Default.MDropbox_upload)
                SaveMode.Dropbox(_bmpScreenShot, _FileName);

            notifyIcon(Type, FileName);

            Log.WriteEvent(String.Format("{0}: {1} elkészült!", Type, FileName));
        }
    }
}
