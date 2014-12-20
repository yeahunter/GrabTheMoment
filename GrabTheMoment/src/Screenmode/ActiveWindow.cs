using System;
using System.Drawing;

namespace GrabTheMoment.ScreenMode
{
    public class ActiveWindow : PrintScreenType
    {
        public ActiveWindow(Rectangle rectangle)
        {
            Type = "ActiveWindow";
            SetFileName();
            X = rectangle.X;
            Y = rectangle.Y;

            Height = rectangle.Height - Y;
            Width = rectangle.Width - X;

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
        }
    }
}
