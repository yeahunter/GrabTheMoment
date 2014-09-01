using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrabTheMoment.ScreenMode
{
    public class RectangleArea : PrintScreenType
    {
        public RectangleArea(Rectangle rectangle)
        {
            SetFileName();
            Height = rectangle.Height - 1;
            Width = rectangle.Width - 1;

            SetXandY();
            X += rectangle.X + 1;
            Y += rectangle.Y + 1;

            CreatePic();
            SavePic();

            notifyIcon(7000, "RectangleArea" + " + " + allmode.WhatClipboard(), FileName, ToolTipIcon.Info);
            Log.WriteEvent("RectangleArea/Constructor: " + FileName + " elkészült!");
        }
    }
}
