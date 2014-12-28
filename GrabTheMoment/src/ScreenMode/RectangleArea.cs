using System;
using System.Drawing;

namespace GrabTheMoment.ScreenMode
{
    public class RectangleArea : PrintScreenType
    {
        public const int LineWidth = 1;

        public RectangleArea(Rectangle rectangle)
        {
            Type = "RectangleArea";
            SetFileName();
            Height = rectangle.Height - LineWidth;
            Width = rectangle.Width - LineWidth;

            SetXandY();
            X += rectangle.X + LineWidth;
            Y += rectangle.Y + LineWidth;

            CreatePic();
            SavePic();
        }
    }
}
