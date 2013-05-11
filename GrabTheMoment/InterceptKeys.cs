﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace GrabTheMoment
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101; // Nemkezel több gombot egyszerre!
        private const int WM_SYSKEYDOWN = 0x0104; // Az Alt-hoz kellett!
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static Form1 windowsform = null;

        public static void Hook(Form1 formegy)
        {
            windowsform = formegy;
            _hookID = SetHook(_proc);
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
                        //windowsform.FullPS();
                        //windowsform.DXFullPS();
                    }
                    else if ((wParam == (IntPtr)260 && Keys.Alt == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        IntPtr hWnd = GetForegroundWindow();
                        Rectangle rect;
                        GetWindowRect(hWnd, out rect);
                        windowsform.WindowPs(rect);
                    }
                    // Lassan rajzolja újra a téglalapot, így msot ezt a funkciót egyenlőre nem használom
                    //else if ((wParam == (IntPtr)256 && Keys.Control == Control.ModifierKeys && number == Keys.PrintScreen))
                    //{
                    //    Form2 secondForm = new Form2();
                    //    secondForm.Show();
                    //}
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
#endif
    }
}