using GrabTheMoment.API;
using GrabTheMoment.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GrabTheMoment
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101; // Nemkezel több gombot egyszerre!
        private const int WM_SYSKEYDOWN = 0x0104; // Az Alt-hoz kellett!
        private static NativeWin32.LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static Main windowsform = null;
        private static string clipboard = null;

        public static void Hook(Main formegy)
        {
            windowsform = formegy;
            _hookID = SetHook(_proc);
        }

        public static void Klipbood()
        {
            if (clipboard != null && windowsform.lastLinkToolStripMenuItem.Enabled)
            {
                Clipboard.SetText(clipboard);
                Log.WriteEvent("Klipbood-0arg: " + clipboard);
            }
            else
                Log.WriteEvent("Klipbood-0arg ures clipboard valtozo");
        }

        public static void Klipbood(string clipboord)
        {
            clipboard = clipboord;

            if (Settings.Default.InstantClipboard)
                Clipboard.SetText(clipboord);

            windowsform.lastLinkToolStripMenuItem.Enabled = true;
            Log.WriteEvent("Klipbood-1arg: " + clipboard);
        }

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

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Keys number = (Keys)Marshal.ReadInt32(lParam);

                string cucc = nCode.ToString() + " | " + 
                            wParam.ToString() + " | " + 
                            lParam.ToString() + " | " + 
                            number.ToString() + " | " + 
                            Control.ModifierKeys.ToString();

                if (number == Keys.PrintScreen)
                {
                    if ((wParam == (IntPtr)256 && number == Keys.PrintScreen && Keys.None == Control.ModifierKeys))
                    {
                        System.Threading.Thread fullps = new System.Threading.Thread(() => new ScreenMode.FullScreen());
                        fullps.SetApartmentState(System.Threading.ApartmentState.STA);
                        fullps.Start();
                    }
                    else if ((wParam == (IntPtr)260 && Keys.Alt == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        IntPtr hWnd = NativeWin32.GetForegroundWindow();
                        Rectangle rect;
                        NativeWin32.GetWindowRect(hWnd, out rect);
                        new Thread(() => new ScreenMode.ActiveWindow(rect)).Start();
                    }
                    else if ((wParam == (IntPtr)256 && Keys.Control == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        DesignateArea secondForm = new DesignateArea();
                        secondForm.Show();
                    }
                }

            }
            return NativeWin32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
#else
#endif
    }
}
