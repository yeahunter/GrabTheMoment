using System;
using System.Drawing;
using System.Windows.Forms;

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

#if !__MonoCS__
            Width = rectangle.Width - X;
            Height = rectangle.Height - Y;
#else
            Width = rectangle.Width;
            Height = rectangle.Height;
#endif

            if (X == -8 && Y == -8)
            {
                X += 8;
                Y += 8;
                Width -= 16;
                Height -= 16;
            }

            X = X < 0 ? 0 : X;
            Y = Y < 0 ? 0 : Y;

            if(X > SystemInformation.VirtualScreen.Width || Y > SystemInformation.VirtualScreen.Height)
                return;

            Width = X < 0 ? Width + X : Width;
            Width = X + Width > SystemInformation.VirtualScreen.Width ? SystemInformation.VirtualScreen.Width - Width : Width;
            Height = Y < 0 ? Height + Y : Height;
            Height = Y + Height > SystemInformation.VirtualScreen.Height ? SystemInformation.VirtualScreen.Height - Height : Height;
            Log.WriteEvent(String.Format("X: {0} | Y: {1} | Width: {2} | Height: {3}", X, Y, Width, Height));

            CreatePic();

            SavePic();
        }
    }
}
