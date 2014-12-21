using GrabTheMoment.Properties;
using System;

namespace GrabTheMoment
{
    class General
    {
        public enum SaveMode
        {
            Disabled,
            Local,
            FTP,
            Dropbox,
            ImgurAnon
        }

        public static string WhatClipboard()
        {
            return ((SaveMode)Settings.Default.CopyLink).ToString();
        }

        // Beallitja a CopyLink erteket
        public static void SetClipboardType(SaveMode Type)
        {
            Settings.Default.CopyLink = (int)Type;
            Settings.Default.Save();
        }
    }
}
