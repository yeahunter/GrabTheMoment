using System;
using System.Drawing;
using System.Windows.Forms;
using GrabTheMoment.Properties;

namespace GrabTheMoment.Screenmode
{
    static class allmode
    {
        public static int x, y;

        public enum CopyType
        {
            Disabled,
            Local,
            FTP,
            Dropbox,
            Imgur
        }

        public static string WhatClipboard()
        {
            return ((CopyType)Settings.Default.CopyLink).ToString();
        }

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
