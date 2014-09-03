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
            trayIcon.Tooltip = "Hello World Icon";
        }

        public void SetVisible(bool Visible)
        {
            trayIcon.Visible = Visible;
        }

        // Create the popup menu, on right click.
        private void OnTrayIconPopup (object o, EventArgs args)
        {
            Menu popupMenu = new Menu();
            ImageMenuItem menuItemDesktopPrint = new ImageMenuItem("Print Desktop");
            //Gtk.Image appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemDesktopPrint.Image = appimg;
            popupMenu.Add(menuItemDesktopPrint);

            menuItemDesktopPrint.Activated += delegate
            {
                InterceptKeys.PrintDesktop();
            };

            ImageMenuItem menuItemActiveWindow = new ImageMenuItem("Print Active Window");
            //appimg = new Gtk.Image(Stock.Info, IconSize.Menu);
            //menuItemActiveWindow.Image = appimg;
            popupMenu.Add(menuItemActiveWindow);

            menuItemActiveWindow.Activated += delegate
            {
                InterceptKeys.PrintActiveWindow();
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
                    var item = new MenuItem(GetWindowText(sc));

                    SItemList.Add(item);
                    Ssubmenu.Append(item);

                    item.ButtonPressEvent += delegate
                    {
                        sc.Show();
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

            /*menuItemSelectWindow.Activated += delegate
            {

            };*/

            SeparatorMenuItem separator = new SeparatorMenuItem();
            popupMenu.Add(separator);

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


        /// <summary>
        /// Gets the text of the window.
        /// </summary>
        /// <param name="windowPointer">The pointer to the window.</param>
        /// <returns>The text of the window.</returns>
        public string GetWindowText(Gdk.Window windowPointer)
        {
            string windowText = string.Empty;
            IntPtr xid = gdk_x11_drawable_get_xid(windowPointer.Handle);
            IntPtr display = gdk_x11_get_default_xdisplay();
            IntPtr namePointer = IntPtr.Zero;
            int success = XFetchName(display, xid, ref namePointer);
            string name = Marshal.PtrToStringAuto(namePointer);

            if (success != 0)
                windowText = name;

            XFree(namePointer);
            return windowText;
        }

        // Ezeket a függvényeket át kellene helyezni egy közös osztályba hogy globálisan lehessen használni őket.
        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_drawable_get_xid(IntPtr window);

        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_get_default_xdisplay();

        [DllImport("gdk-x11-2.0")]
        private static extern void gdk_error_trap_push();

        [DllImport("libX11", EntryPoint = "XFetchName")]
        private static extern int XFetchName(IntPtr display, IntPtr window, ref IntPtr window_name);

        [DllImport("libX11", EntryPoint = "XFree")]
        private static extern int XFree(IntPtr data);
    }
}
#endif