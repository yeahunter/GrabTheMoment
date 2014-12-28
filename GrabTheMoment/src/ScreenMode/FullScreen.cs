using System;
using System.Windows.Forms;

namespace GrabTheMoment.ScreenMode
{
    public class FullScreen : PrintScreenType
    {
        public FullScreen()
        {
            Type = "FullScreen";
            SetFileName();
            Height = SystemInformation.VirtualScreen.Height;
            Width = SystemInformation.VirtualScreen.Width;

            SetXandY();
            CreatePic();

            SavePic();
        }
    }
}
