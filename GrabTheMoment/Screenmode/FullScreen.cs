using System;
using System.Windows.Forms;

namespace GrabTheMoment.Screenmode
{
    public class FullScreen : PrintScreenType
    {
        public FullScreen()
        {
            SetFileName();
            Height = SystemInformation.VirtualScreen.Height;
            Width = SystemInformation.VirtualScreen.Width;

            SetXandY();
            CreatePic();

            SavePic();

            notifyIcon(7000, "FullScreen" + " + " + allmode.WhatClipboard(), FileName, ToolTipIcon.Info);
            Log.WriteEvent("FullScreen/Constructor: " + FileName + " elkészült!");
        }
    }
}
