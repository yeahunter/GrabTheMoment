using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Gameshot
{
    class DllImports
    {
        private static bool _HookStarted = false;
        private static Label _l;
        public static void OdaIrjak(Label l)
        {
            _l = l;
        }
        
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101; // Nemkezel több gombot egyszerre!
        private const int WM_SYSKEYDOWN = 0x0104; // Az Alt-hoz kellett!

        static IntPtr hookId = IntPtr.Zero;

        static IntPtr SetHook(LowLevelKeyboardProc proc)
        {

            using (Process process = Process.GetCurrentProcess())
            {
                using (ProcessModule module = process.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
                }
            }
        }

        static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Keys number = (Keys)Marshal.ReadInt32(lParam);
                //int vkCode = Marshal.ReadInt32(lParam);
                //Debug.WriteLine((Keys)vkCode);
                //_l.Text = nCode.ToString() + " | " + wParam.ToString() + " | " + lParam.ToString() + " | " + number.ToString() + " | " + Control.ModifierKeys.ToString();
                if (number == Keys.A)
                    _l.Text = GetActiveWindowTitle();
            }
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        public static void HookStart()
        {
            if (!_HookStarted)
            {
                hookId = SetHook(HookCallback);
                _HookStarted = true;
            }
        }

        public static void HookStop()
        {
            if (_HookStarted)
            {
                UnhookWindowsHookEx(hookId);
                _HookStarted = false;
            }
        }  
    }
}
