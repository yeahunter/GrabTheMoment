using System;
using System.Windows.Forms;

namespace GrabTheMoment.Screenmode
{
    public class FullScreen : PrintScreenType
    {
        public FullScreen()
        {
            SetFileName();
            SetImgSize(SystemInformation.VirtualScreen.Height, SystemInformation.VirtualScreen.Width);
            CreatePic();

            SavePic();

            notifyIcon(7000, "FullPS" + " + " + allmode.WhatClipboard(), FileName, ToolTipIcon.Info);
            Log.WriteEvent("Form1/FullPS: " + FileName + " elkészült!");
        }
    }
}
