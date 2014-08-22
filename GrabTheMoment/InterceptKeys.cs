using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
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
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
#else
        private static SpecialKeys special = new SpecialKeys();
#endif

        private static Form1 windowsform = null;
        private static string clipboard = null;

        public static void Hook(Form1 formegy)
        {
            windowsform = formegy;
#if !__MonoCS__
            _hookID = SetHook(_proc);
#endif
        }

#if __MonoCS__
        public static void InitLinux()
        {
            special.RegisterHandler(SpecialPrint, SpecialKey.Print);
            special.RegisterHandler(SpecialAltPrint, SpecialKey.AltPrint);
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
                Gtk.Clipboard clip = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
                clip.Text = text;
            }
            catch
            {

            }
        }
#endif

        public static Form1 windowsformoscucc
        {
            get { return windowsform; }
        }

#if !__MonoCS__
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

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
                       Thread fullps = new Thread(() => Screenmode.allmode.FullPS());
                        fullps.SetApartmentState(ApartmentState.STA);
                        fullps.Start();
                        //new Thread(() => screenmode.FullPS()).Start();
                        //windowsform.DXFullPS();
                    }
                    else if ((wParam == (IntPtr)260 && Keys.Alt == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        IntPtr hWnd = GetForegroundWindow();
                        Rectangle rect;
                        GetWindowRect(hWnd, out rect);
                        new Thread(() => Screenmode.allmode.WindowPs(rect)).Start();
                    }
                    // Lassan rajzolja újra a téglalapot, így msot ezt a funkciót egyenlőre nem használom
                    else if ((wParam == (IntPtr)256 && Keys.Control == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        //Thread areaps = new Thread(() => new Form2());
                        //areaps.SetApartmentState(ApartmentState.STA);
                        //areaps.Start();
                        Form2 secondForm = new Form2();
                        secondForm.Show();
                    }
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowRect(IntPtr hWnd, out Rectangle rect);
#else
        public static void PrintDesktop()
        {
            // Csak asztalt lehet vele egyenlőre fényképezni
            Thread fullps = new Thread(() => Screenmode.allmode.FullPS());
            fullps.SetApartmentState(ApartmentState.STA);
            fullps.Start();
        }

        public static void PrintActiveWindow()
        {
            int x;
            int y;
            int width;
            int height;
            int depth;
            Rectangle rect;

            Gdk.Screen.Default.ActiveWindow.GetGeometry(out x, out y, out width, out height, out depth);
            Gdk.Screen.Default.ActiveWindow.GetRootOrigin(out x, out y);

            /*if(x < 0)
            {
                Gdk.Screen.Default.ActiveWindow.Move(0, y);
                Gdk.Screen.Default.ActiveWindow.GetGeometry(out x, out y, out width, out height, out depth);
                Gdk.Screen.Default.ActiveWindow.GetRootOrigin(out x, out y);
            }

            if(y < 0)
            {
                Gdk.Screen.Default.ActiveWindow.Move(x, 0);
                Gdk.Screen.Default.ActiveWindow.GetGeometry(out x, out y, out width, out height, out depth);
                Gdk.Screen.Default.ActiveWindow.GetRootOrigin(out x, out y);
            }*/

            // Ha nem látszi az ablak egy része mert kiment a képernyőről akkor az összeomlást elkerülendően
            // az ablakból annyi fog csak látszódni amennyi a képernyőn is látszik.
            rect = new Rectangle(x < 0 ? 0 : x, y < 0 ? 0 : y, x < 0 ? width + x : width, y < 0 ? height + y : height);
            //rect = new Rectangle(x, y, width, height);
            new Thread(() => Screenmode.allmode.WindowPs(rect)).Start();
        }

        private static void SpecialPrint(object o, SpecialKey key)
        {
            Log.WriteEvent("Hotkey Pressed! [Print]");
            PrintDesktop();
        }

        private static void SpecialAltPrint(object o, SpecialKey key)
        {
            Log.WriteEvent("Hotkey Pressed! [Alt+Print]");
            PrintActiveWindow();
        }
#endif
    }
}
