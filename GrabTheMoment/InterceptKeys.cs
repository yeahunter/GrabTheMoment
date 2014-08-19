using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
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
        private static Screenmode.allmode screenmode = null;
        private static string clipboard = null;

        public static void Hook(Form1 formegy)
        {
            windowsform = formegy;
            screenmode = new Screenmode.allmode();
#if !__MonoCS__
            _hookID = SetHook(_proc);
#endif
        }

#if __MonoCS__
        public static void InitLinux()
        {
            special.RegisterHandler(Specialsdf, SpecialKey.asd);
        }
#endif

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

        public static Form1 windowsformoscucc
        {
            get { return windowsform; }
        }

        public static Screenmode.allmode smode
        {
            get { return screenmode; }
            set { screenmode = value; }
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
                        System.Threading.Thread fullps = new System.Threading.Thread(() => screenmode.FullPS());
                        fullps.SetApartmentState(System.Threading.ApartmentState.STA);
                        fullps.Start();
                        //new System.Threading.Thread(() => screenmode.FullPS()).Start();
                        //windowsform.DXFullPS();
                    }
                    else if ((wParam == (IntPtr)260 && Keys.Alt == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        IntPtr hWnd = GetForegroundWindow();
                        Rectangle rect;
                        GetWindowRect(hWnd, out rect);
                        new System.Threading.Thread(() => screenmode.WindowPs(rect)).Start();
                    }
                    // Lassan rajzolja újra a téglalapot, így msot ezt a funkciót egyenlőre nem használom
                    else if ((wParam == (IntPtr)256 && Keys.Control == Control.ModifierKeys && number == Keys.PrintScreen))
                    {
                        //System.Threading.Thread areaps = new System.Threading.Thread(() => new Form2());
                        //areaps.SetApartmentState(System.Threading.ApartmentState.STA);
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
        private static void Specialsdf(object o, SpecialKey key)
        {
            Log.WriteEvent("Hotkey Pressed!");
            // Csak asztalt lehet vele egyenlőre fényképezni
            System.Threading.Thread fullps = new System.Threading.Thread(() => screenmode.FullPS());
            fullps.SetApartmentState(System.Threading.ApartmentState.STA);
            fullps.Start();
        }
#endif
    }

#if __MonoCS__
    public delegate void SpecialKeyPressedHandler(object o, SpecialKey key);

    public enum SpecialKey
    {
        asd = Gdk.Key.Page_Up

    };

    public class SpecialKeys
    {
        private Hashtable key_map = new Hashtable();
        private Hashtable key_registrations = new Hashtable();
        private IEnumerable keycode_list;
        private TimeSpan raise_delay = new TimeSpan(0);
        private DateTime last_raise = DateTime.MinValue;

        public SpecialKeys()
        {
            Log.WriteEvent("init");
            keycode_list = BuildKeyCodeList();
            InitializeKeys();
        }

        public void Dispose()
        {
            UnitializeKeys();
        }

        public void RegisterHandler(SpecialKeyPressedHandler handler, params SpecialKey [] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys) {
                if(key_map.Contains(specialKey)) {
                    int key = (int)key_map[specialKey];
                    key_registrations[key] = Delegate.Combine(key_registrations[key] as Delegate, handler);
                }
            }
        }

        public void UnregisterHandler(SpecialKeyPressedHandler handler, params SpecialKey [] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys) {
                if(key_map.Contains(specialKey)) {
                    int key = (int)key_map[specialKey];
                    key_registrations[key] = Delegate.Remove(key_registrations[key] as Delegate, handler); 
                }
            }
        }

        private IEnumerable BuildKeyCodeList()
        {
            ArrayList kc_list = new ArrayList();

            foreach(SpecialKey key in Enum.GetValues(typeof(SpecialKey))) {
                IntPtr xdisplay = gdk_x11_get_default_xdisplay();

                if(!xdisplay.Equals(IntPtr.Zero)) {
                    int keycode = XKeysymToKeycode(xdisplay, key);
                    if(keycode != 0) {
                        key_map[keycode] = key;
                        key_map[key] = keycode;
                        kc_list.Add(keycode);
                    }
                }
            }
            return kc_list;
        }

        private void InitializeKeys()
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++) {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);

                foreach(int keycode in keycode_list) {
                    GrabKey(screen.RootWindow, keycode);
                }

                screen.RootWindow.AddFilter(FilterKey);
            }


        }

        private void UnitializeKeys() 
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++) {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);
                foreach(int keycode in keycode_list) {
                    UngrabKey(screen.RootWindow, keycode);
                }
                screen.RootWindow.RemoveFilter(FilterKey);
            }
        }

        private void GrabKey(Gdk.Window root, int keycode)
        {   

            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            XGrabKey(xdisplay, keycode, XModMask.None, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod2, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod5, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Lock, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Mod5, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Lock, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod5 | XModMask.Lock, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Mod5 | XModMask.Lock, xid, true, 
                XGrabMode.Async, XGrabMode.Async);

            gdk_flush();

            if(gdk_error_trap_pop() != 0) {
                Log.WriteEvent(string.Format(": Could not grab key {0} (maybe another application has grabbed this key)", keycode));
            }
        }

        private void UngrabKey(Gdk.Window root, int keycode)
        {
            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            XUngrabKey(xdisplay, keycode, XModMask.None, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod2, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod5, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Lock, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Mod5, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Lock, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod5 | XModMask.Lock, xid);
            XUngrabKey(xdisplay, keycode, XModMask.Mod2 | XModMask.Mod5 | XModMask.Lock,xid);

            gdk_flush();

            if(gdk_error_trap_pop() != 0) {
                Log.WriteEvent(string.Format(": Could not ungrab key {0} (maybe another application has grabbed this key)", keycode));
            }
        }

        private Gdk.FilterReturn FilterKey(IntPtr xeventPtr, Gdk.Event gdkEvent)
        {
            Log.WriteEvent("filter "+ gdkEvent.Type);
            if(DateTime.Now - last_raise < raise_delay) {
                return Gdk.FilterReturn.Continue;
            }

            last_raise = DateTime.Now;

            XKeyEvent xevent = (XKeyEvent)Marshal.PtrToStructure(xeventPtr, typeof(XKeyEvent));

            if(xevent.type != XEventName.KeyPress) {
                return Gdk.FilterReturn.Continue;
            }

            int keycode = (int)xevent.keycode;
            object x = key_map[keycode];

            Log.WriteEvent("filter "+ keycode);

            if(x == null) {
                return Gdk.FilterReturn.Continue;
            }

            SpecialKey key = (SpecialKey)key_map[keycode];

            if(key_registrations[keycode] != null) {
                x = key_registrations[keycode];
                if(x is SpecialKeyPressedHandler) {
                    ((SpecialKeyPressedHandler)x)(this, key);    
                }
                return Gdk.FilterReturn.Remove;
            }

            return Gdk.FilterReturn.Continue;
        }

        public TimeSpan Delay {
            get {
                return raise_delay;
            }

            set {
                raise_delay = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct XKeyEvent
        {
            public XEventName type;
            public IntPtr serial;
            public bool send_event;
            public IntPtr display;
            public IntPtr window;
            public IntPtr root;
            public IntPtr subwindow;
            public IntPtr time;
            public int x;
            public int y;
            public int x_root;
            public int x_y;
            public uint state;
            public uint keycode;
            public bool same_screen;
        }

        [DllImport("libX11")]
        private static extern int XKeysymToKeycode(IntPtr display, SpecialKey keysym);

        [DllImport("libX11")]
        private static extern void XGrabKey(IntPtr display, int keycode, XModMask modifiers, 
            IntPtr window, bool owner_events, XGrabMode pointer_mode, XGrabMode keyboard_mode);

        [DllImport("libX11")]
        private static extern void XUngrabKey(IntPtr display, int keycode, XModMask modifiers, 
            IntPtr window);

        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_drawable_get_xid(IntPtr window);

        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_get_default_xdisplay();

        [DllImport("gdk-x11-2.0")]
        private static extern void gdk_error_trap_push();

        [DllImport("gdk-x11-2.0")]
        private static extern int gdk_error_trap_pop();

        [DllImport("gdk-x11-2.0")]
        private static extern void gdk_flush();

        [Flags]
        private enum XModMask {
            None    = 0,
            Shift   = 1 << 0,
            Lock    = 1 << 1,
            Control = 1 << 2,
            Mod1    = 1 << 3,
            Mod2    = 1 << 4,
            Mod3    = 1 << 5,
            Mod4    = 1 << 6,
            Mod5    = 1 << 7
        }

        private enum XGrabMode {
            Sync  = 0,
            Async = 1
        }

        private enum XEventName {
            KeyPress   = 2,
            KeyRelease = 3,
        }
    }
#endif
}
