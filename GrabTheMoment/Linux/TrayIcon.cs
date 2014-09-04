#if __MonoCS__
using System;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Gtk;
using Gdk;

using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public class TrayIcon
    {
        // The tray Icon
        private List<MenuItem> SItemList = new List<MenuItem>();
        private static StatusIcon trayIcon;
        public static StatusIcon GetTrayIcon() { return trayIcon; }

        public TrayIcon ()
        {
        }

        public void Init()
        {
            // Creation of the Icon
            MemoryStream memoryStream = new MemoryStream();
            InterceptKeys.windowsformoscucc.YeahunterIcon.ToBitmap().Save(memoryStream, ImageFormat.Png);
            trayIcon = new StatusIcon(new Pixbuf(memoryStream.ToArray()));
            trayIcon.Visible = true;

            // Show/Hide the window (even from the Panel/Taskbar) when the TrayIcon has been clicked.
            trayIcon.Activate += delegate
            {
                InterceptKeys.windowsformoscucc.SetVisible(!InterceptKeys.windowsformoscucc.Visible);
            };

            // Show a pop up menu when the icon has been right clicked.
            trayIcon.PopupMenu += OnTrayIconPopup;

            // A Tooltip for the Icon
            trayIcon.Tooltip = "Grab The Moment";
        }

        public void SetVisible(bool Visible)
        {
            trayIcon.Visible = Visible;
        }

        // Create the popup menu, on right click.
        private void OnTrayIconPopup (object o, EventArgs args)
        {
            Menu popupMenu = new Menu();
            ImageMenuItem menuItemShowMainWindow = new ImageMenuItem("Show main window");
            //Gtk.Image appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemShowMainWindow.Image = appimg;
            popupMenu.Add(menuItemShowMainWindow);

            menuItemShowMainWindow.Activated += delegate
            {
                InterceptKeys.windowsformoscucc.SetVisible(true);
            };

            SeparatorMenuItem separator = new SeparatorMenuItem();
            popupMenu.Add(separator);

            ImageMenuItem menuItemDesktopPrint = new ImageMenuItem("Print Desktop");
            //Gtk.Image appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemDesktopPrint.Image = appimg;
            popupMenu.Add(menuItemDesktopPrint);

            menuItemDesktopPrint.Activated += delegate
            {
                popupMenu.Cancel();

                new Thread(() =>
                {
                    Thread.Sleep(400); // Kis késleltetés.
                    InterceptKeys.PrintDesktop();
                }).Start();
            };

            ImageMenuItem menuItemActiveWindow = new ImageMenuItem("Print Active Window");
            //appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemActiveWindow.Image = appimg;
            popupMenu.Add(menuItemActiveWindow);

            menuItemActiveWindow.Activated += delegate
            {
                popupMenu.Cancel();

                new Thread(() =>
                {
                    Thread.Sleep(400); // Kis késleltetés.
                    InterceptKeys.PrintActiveWindow();
                }).Start();
            };

            ImageMenuItem menuItemDesignateArea = new ImageMenuItem("Print Designate Area");
            //appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemDesignateArea.Image = appimg;
            popupMenu.Add(menuItemDesignateArea);

            menuItemDesignateArea.Activated += delegate
            {
                popupMenu.Cancel();
                InterceptKeys.PrintDesignateArea();
            };

            ImageMenuItem menuItemSelectWindow = new ImageMenuItem("Select Windows");
            //appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemActiveWindow.Image = appimg;
            popupMenu.Add(menuItemSelectWindow);

            Menu Ssubmenu = new Menu();
            menuItemSelectWindow.Submenu = Ssubmenu;

            menuItemSelectWindow.Submenu.Focused += delegate
            {
                foreach(var sc in Gdk.Screen.Default.WindowStack)
                {
                    var item = new MenuItem(NativeLinux.GetWindowText(sc));

                    SItemList.Add(item);
                    Ssubmenu.Append(item);

                    item.ButtonPressEvent += delegate
                    {
                        // Így rendesen felugranak az ablakok.
                        sc.Focus(0);
                        // Bezárja az almenüt.
                        Ssubmenu.Cancel();
                        // Bezárja a főmenüt.
                        popupMenu.Cancel();

                        new Thread(() =>
                        {
                            Thread.Sleep(400); // Kis késleltetés hogy az ablakok megtudjanak időben jelenni.
                            InterceptKeys.PrintWindow(sc);
                        }).Start();
                    };
                }

                Ssubmenu.ShowAll();
                Ssubmenu.Popup();
            };

            menuItemSelectWindow.Submenu.Hidden += delegate
            {
                foreach(var sc in Gdk.Screen.Default.WindowStack)
                {
                    foreach(var list in SItemList)
                        list.Destroy();

                    SItemList.Clear();
                }
            };

            SeparatorMenuItem separator2 = new SeparatorMenuItem();
            popupMenu.Add(separator2);

            ImageMenuItem menuItemQuit = new ImageMenuItem("Quit");
            Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
            menuItemQuit.Image = appimg;
            popupMenu.Add(menuItemQuit);

            // Quit the application when quit has been clicked.
            menuItemQuit.Activated += delegate
            {
                InterceptKeys.UninitLinux();
                System.Windows.Forms.Application.Exit();
                Application.Quit();
            };

            popupMenu.ShowAll();
            popupMenu.Popup();
        }
    }
}
#endif