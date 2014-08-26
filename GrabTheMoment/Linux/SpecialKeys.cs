#if __MonoCS__
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public class SpecialKeys
    {
        private Dictionary<int, HookKey> KeyMap = new Dictionary<int, HookKey>();
        private DateTime last_raise = DateTime.MinValue;
        private TimeSpan raise_delay = new TimeSpan(0);
        private const int NoneKey = -88;

        public SpecialKeys()
        {
            Log.WriteEvent("SpecialKeys: Init");
            BuildKeyMap();
            InitializeKeys();
        }

        public void Dispose()
        {
            Log.WriteEvent("SpecialKeys: Dispose");
            UnitializeKeys();
            KeyMap.Clear();
        }

        public void RegisterHandler(SpecialKeyPressedHandler handler, Gdk.ModifierType ModeMask, params SpecialKey[] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys)
            {
                int key = NoneKey;

                if((key = Contains(specialKey)) != NoneKey)
                {
                    if(KeyMap[key].ModeMask == Gdk.ModifierType.None && ModeMask != Gdk.ModifierType.None)
                        KeyMap[key].ModeMask = ModeMask;

                    KeyMap[key].SKPHMethod += handler;
                    KeyMap[key].SKPHMethod = handler;
                    ReinitializeKeys();
                }
            }
        }

        public void UnregisterHandler(SpecialKeyPressedHandler handler, params SpecialKey[] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys)
            {
                int key = (int)specialKey;

                if(KeyMap.ContainsKey(key))
                    KeyMap[key].SKPHMethod -= handler;
            }
        }

        private void ReinitializeKeys()
        {
            UnitializeKeys();
            InitializeKeys();
        }

        private int Contains(SpecialKey key)
        {
            foreach(var map in KeyMap)
            {
                if(map.Value.SKey == key)
                    return map.Value.Key;
            }

            return NoneKey;
        }

        private void BuildKeyMap()
        {
            foreach(SpecialKey key in Enum.GetValues(typeof(SpecialKey)))
            {
                IntPtr xdisplay = gdk_x11_get_default_xdisplay();

                if(!xdisplay.Equals(IntPtr.Zero))
                {
                    int keycode = XKeysymToKeycode(xdisplay, key);
                    if(keycode != 0)
                        KeyMap.Add(keycode, new HookKey(keycode, key));
                }
            }
        }

        private void InitializeKeys()
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++)
            {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);

                foreach(var map in KeyMap)
                    GrabKey(screen.RootWindow, map.Key, map.Value.ModeMask);

                screen.RootWindow.AddFilter(FilterKey);
            }
        }

        private void UnitializeKeys() 
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++)
            {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);

                foreach(var map in KeyMap)
                    UngrabKey(screen.RootWindow, map.Key, map.Value.ModeMask);

                screen.RootWindow.RemoveFilter(FilterKey);
            }
        }

        private void GrabKey(Gdk.Window root, int keycode, Gdk.ModifierType modemask = Gdk.ModifierType.None)
        {
            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            XGrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, modemask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, modemask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);

            gdk_flush();

            if(gdk_error_trap_pop() != 0)
                Log.WriteEvent(string.Format("SpecialKeys: Could not grab key {0} (maybe another application has grabbed this key)", keycode));
        }

        private void UngrabKey(Gdk.Window root, int keycode, Gdk.ModifierType modemask = Gdk.ModifierType.None)
        {
            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid);
            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid);
            XUngrabKey(xdisplay, keycode, modemask, xid);
            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid);
            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask, xid);
            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid);
            XUngrabKey(xdisplay, keycode, modemask | Gdk.ModifierType.LockMask, xid);
            XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask | Gdk.ModifierType.LockMask, xid);

            gdk_flush();

            if(gdk_error_trap_pop() != 0)
                Log.WriteEvent(string.Format("SpecialKeys: Could not ungrab key {0} (maybe another application has grabbed this key)", keycode));
        }

        private Gdk.FilterReturn FilterKey(IntPtr xeventPtr, Gdk.Event gdkEvent)
        {
            Log.WriteEvent("SpecialKeys: [1]filter " + gdkEvent.Type);

            if(DateTime.Now - last_raise < raise_delay)
                return Gdk.FilterReturn.Continue;

            last_raise = DateTime.Now;
            XKeyEvent xevent = (XKeyEvent)Marshal.PtrToStructure(xeventPtr, typeof(XKeyEvent));

            if(xevent.type != XEventName.KeyPress)
                return Gdk.FilterReturn.Continue;

            int keycode = (int)xevent.keycode;
            object x = KeyMap[keycode];

            Log.WriteEvent("SpecialKeys: [2]filter " + keycode);

            if(x == null)
                return Gdk.FilterReturn.Continue;

            if(KeyMap[keycode].SKPHMethod.Method != null)
            {
                KeyMap[keycode].SKPHMethod.Invoke(this, KeyMap[keycode].SKey, (Gdk.ModifierType)xevent.state);
                return Gdk.FilterReturn.Remove;
            }

            return Gdk.FilterReturn.Continue;
        }

        public TimeSpan Delay
        {
            get { return raise_delay; }
            set { raise_delay = value; }
        }

        [DllImport("libX11")]
        private static extern int XKeysymToKeycode(IntPtr display, SpecialKey keysym);

        [DllImport("libX11")]
        private static extern void XGrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, 
            IntPtr window, bool owner_events, XGrabMode pointer_mode, XGrabMode keyboard_mode);

        [DllImport("libX11")]
        private static extern void XUngrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, IntPtr window);

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
    }
}
#endif