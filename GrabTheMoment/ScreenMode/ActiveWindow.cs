using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrabTheMoment.ScreenMode
{
    public class ActiveWindow : PrintScreenType
    {
        public ActiveWindow(Rectangle rectangle)
        {
            SetFileName();
#if !__MonoCS__
            Width = rectangle.Width - X;
            Height = rectangle.Height - Y;
#else
            Width = rectangle.Width;
            Height = rectangle.Height;
#endif

            X = rectangle.X;
            Y = rectangle.Y;

            if (X == -8 && Y == -8)
            {
                X += 8;
                Y += 8;
                Width -= 16;
                Height -= 16;
            }
            Log.WriteEvent(String.Format("X: {0} | Y: {1} | Width: {2} | Height: {3}", X, Y, Width, Height));


            CreatePic();

            SavePic();

            notifyIcon(7000, "ActiveWindow" + " + " + allmode.WhatClipboard(), FileName, ToolTipIcon.Info);
            Log.WriteEvent("ActiveWindow/Constructor: " + FileName + " elkészült!");
        }
    }
}
