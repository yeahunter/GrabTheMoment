﻿using System;
#if __MonoCS__
using System.IO;
#endif
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
#if __MonoCS__
using GrabTheMoment.Linux;
#endif

namespace GrabTheMoment
{
    static class Program
    {
        // Fix: (GrabTheMoment:12338): GLib-CRITICAL **: Source ID 13 was not found when attempting to remove it
        // Megakadályozza hogy eltünjön az ikon. (Hibrid megoldásból ered ez a hiba.)
#if __MonoCS__
        static TrayIcon trayicon;
#endif
        static Main windowsform;
        static string AppGUID = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
        static bool futhatoke = false;
#if __MonoCS__
        static string AppName = Assembly.GetExecutingAssembly().ManifestModule.Name;
#endif

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if __MonoCS__
            if (File.Exists(AppName + ".config"))
                File.Delete(AppName + ".config");
#endif

            try
            {
                Mutex mutex = new Mutex(false, "Local\\" + AppGUID);
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Már fut a program!");
                    mutex.ReleaseMutex();
                    mutex = null;
                    return;
                }
                else
                {
                    futhatoke = true;
                }
                
            }
            catch (Exception)
            {
                futhatoke = true;
            }

            if (futhatoke)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                windowsform = new Main();
                InterceptKeys.Hook(windowsform);
#if !__MonoCS__
                Application.Run(windowsform);
#else
                Runtime.SetProcessName(Path.GetFileNameWithoutExtension(AppName));
                new Thread(() => Application.Run(windowsform)).Start();
                Gtk.Application.Init();

                // Attach to the Delete Event when the window has been closed.
                //window.DeleteEvent += delegate { Gtk.Application.Quit (); }; // bugos a WINFORM leállítása mert gtk-t nem lővi le.
                trayicon = new TrayIcon();
                trayicon.Init();
                InterceptKeys.InitLinux();

                // Show the main window and start the application.
                //window.ShowAll ();
                Gtk.Application.Run();
#endif
            }
        }
    }
}
