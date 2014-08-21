#if __MonoCS__
using System;
using System.IO;
using System.Drawing.Imaging;
using Gtk;
using Gdk;

namespace GrabTheMoment.Linux
{
    public class TrayIcon
    {
        // The tray Icon
        private static StatusIcon trayIcon;
        public static StatusIcon GetTrayIcon() { return trayIcon; }

        public TrayIcon ()
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

            ImageMenuItem menuItemQuit = new ImageMenuItem("Quit");
            Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
            menuItemQuit.Image = appimg;
            popupMenu.Add(menuItemQuit);

            // Quit the application when quit has been clicked.
            menuItemQuit.Activated += delegate
            {
                System.Windows.Forms.Application.Exit();
                Application.Quit();
            };

            popupMenu.ShowAll();
            popupMenu.Popup();
        }
    }
}
#endif