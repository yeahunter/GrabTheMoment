using System;
using System.Windows.Forms;

namespace GrabTheMoment.ScreenMode
{
    static class allmode
    {
        public static int x, y;

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
    }
}
