using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
﻿using GrabTheMoment.API;
#if __MonoCS__
using GrabTheMoment.Linux;
#endif
using GrabTheMoment.Properties;

namespace GrabTheMoment
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101; // Nemkezel több gombot egyszerre!
        private const int WM_SYSKEYDOWN = 0x0104; // Az Alt-hoz kellett!
#if !__MonoCS__
        private static NativeWin32.LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
#else
        private static SpecialKeys special;
        private static Gtk.Clipboard clip;
#endif

        private static Main windowsform = null;
        private static string clipboard = null;

        public static void Hook(Main formegy)
        {
            windowsform = formegy;
#if !__MonoCS__
            _hookID = SetHook(_proc);
#endif
        }

#if __MonoCS__
        public static void InitLinux()
        {
            special = new SpecialKeys();
            special.RegisterHandler(SpecialPrint, Gdk.ModifierType.Mod1Mask, SpecialKey.Print);
            special.RegisterHandler(SpecialPrint, Gdk.ModifierType.ControlMask, SpecialKey.Print);
            clip = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
        }

        public static void UninitLinux()
        {
            special.Dispose();
        }
#endif

        public static void Klipbood()
        {
#if !__MonoCS__
            if (clipboard != null && windowsform.lastLinkToolStripMenuItem.Enabled)
            {
                Clipboard.SetText(clipboard);
                Log.WriteEvent("Klipbood-0arg: " + clipboard);
            }
            else
                Log.WriteEvent("Klipbood-0arg ures clipboard valtozo");
#else
            if (clipboard != null) // TODO
            {
                Gtk.Application.Invoke((delegate { setClipboard(clipboard); }));
                Log.WriteEvent("Klipbood-0arg: " + clipboard);
            }
            else
                Log.WriteEvent("Klipbood-0arg ures clipboard valtozo");
#endif
        }

        public static void Klipbood(string clipboord)
        {
            clipboard = clipboord;
#if !__MonoCS__
            if (Settings.Default.InstantClipboard)
                Clipboard.SetText(clipboard);

            windowsform.lastLinkToolStripMenuItem.Enabled = true;
#else
            // TODO: Miért false mindig?
            //if (Settings.Default.InstantClipboard)
                Gtk.Application.Invoke((delegate { setClipboard(clipboard); }));
#endif
            Log.WriteEvent("Klipbood-1arg: " + clipboard);
        }

#if __MonoCS__
        private static void setClipboard(string text)
        {
            try
            {
                clip.Text = text;
            }
            catch(Exception e)
            {
                Log.WriteEvent("setClipboard: ", e);
            }
        }
#endif

        public static Main windowsformoscucc
        {
            get { return windowsform; }
        }

#if !__MonoCS__
        private static IntPtr SetHook(NativeWin32.LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeWin32.SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    NativeWin32.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 &&
                (wParam == (IntPtr)WM_KEYDOWN ||
                wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Keys number = (Keys)Marshal.ReadInt32(lParam);
                //MessageBox.Show(nCode.ToString() + " | " + wParam.ToString() + " | " + lParam.ToString());

                string cucc = nCode.ToString() + " | " + wParam.ToString() + " | " + lParam.ToString() + " | " + number.ToString() + " | " + Control.ModifierKeys.ToString();
                //windowsform.notifyIcon1.ShowBalloonTip(1000, "Debug", cucc, ToolTipIcon.Info);

                //MessageBox.Show(Control.ModifierKeys.ToString());
                if (number == Keys.PrintScreen)
                {
                    //MessageBox.Show(lParam.ToString());
                    if ((wParam == (IntPtr)256 && number == Keys.PrintScreen && Keys.None == Control.ModifierKeys))
                    {
                        Thread fullps = new Thread(() => new ScreenMode.FullScreen());
                        fullps.SetApartmentState(ApartmentState.STA);
                        fullps.Start();
                        //new Thread(() => ScreenMode.FullPS()).Start();
                        //windowsform.DXFullPS();
                    }
                    else if ((wParam == (IntPtr)260 && Keys.Alt == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        IntPtr hWnd = NativeWin32.GetForegroundWindow();
                        Rectangle rect;
                        NativeWin32.GetWindowRect(hWnd, out rect);
                        new Thread(() => new ScreenMode.ActiveWindow(rect)).Start();
                    }
                    // Lassan rajzolja újra a téglalapot, így msot ezt a funkciót egyenlőre nem használom
                    else if ((wParam == (IntPtr)256 && Keys.Control == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        //Thread areaps = new Thread(() => new Form2());
                        //areaps.SetApartmentState(ApartmentState.STA);
                        //areaps.Start();
                        DesignateArea secondForm = new DesignateArea();
                        secondForm.Show();
                    }
                }

            }
            return NativeWin32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
#else
        public static void PrintDesktop()
        {
            Thread fullps = new Thread(() => new ScreenMode.FullScreen());
            fullps.SetApartmentState(ApartmentState.STA);
            fullps.Start();
        }

        public static void PrintActiveWindow()
        {
            PrintWindow(Gdk.Screen.Default.ActiveWindow);
        }

        public static void PrintWindow(Gdk.Window window)
        {
            // Ha nem látszi az ablak egy része mert kiment a képernyőről akkor az összeomlást elkerülendően
            // az ablakból annyi fog csak látszódni amennyi a képernyőn is látszik.
            int x = window.FrameExtents.X;
            int y = window.FrameExtents.Y;
            int width = window.FrameExtents.Width;
            int height = window.FrameExtents.Height;
            Rectangle rect = new Rectangle(x, y, width, height);
            new Thread(() => new ScreenMode.ActiveWindow(rect)).Start();
        }

        public static void PrintDesignateArea()
        {
            DesignateArea secondForm = new DesignateArea();
            Application.Run(secondForm); // Így normálisan lefut.
        }

        private static void SpecialPrint(object o, SpecialKey key, Gdk.ModifierType ModeMask)
        {
            if(key == SpecialKey.Print && ModeMask == Gdk.ModifierType.Mod2Mask)
            {
                Log.WriteEvent("Hotkey Pressed! [Print]");
                PrintDesktop();
            }
            else if(key == SpecialKey.Print && ModeMask == (Gdk.ModifierType.Mod1Mask | Gdk.ModifierType.Mod2Mask))
            {
                Log.WriteEvent("Hotkey Pressed! [Alt+Print]");
                PrintActiveWindow();
            }
            else if(key == SpecialKey.Print && ModeMask == (Gdk.ModifierType.ControlMask | Gdk.ModifierType.Mod2Mask))
            {
                Log.WriteEvent("Hotkey Pressed! [Control+Print]");
                PrintDesignateArea();
            }
        }
#endif
    }
}
