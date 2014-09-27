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
                    var mm = new Gdk.ModifierType[] { ModeMask };
                    mm.CopyTo(KeyMap[key].ModeMask, 0);
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

                if((key = Contains(specialKey)) != NoneKey)
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
                IntPtr xdisplay = NativeLinux.gdk_x11_get_default_xdisplay();

                if(!xdisplay.Equals(IntPtr.Zero))
                {
                    int keycode = NativeLinux.XKeysymToKeycode(xdisplay, key);
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

        private void GrabKey(Gdk.Window root, int keycode, Gdk.ModifierType[] modemask)
        {
            IntPtr xid = NativeLinux.gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = NativeLinux.gdk_x11_get_default_xdisplay();

            NativeLinux.gdk_error_trap_push();

            foreach(var mmask in modemask)
            {
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, mmask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | mmask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, mmask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                NativeLinux.XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | mmask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            }

            NativeLinux.gdk_flush();

            if(NativeLinux.gdk_error_trap_pop() != 0)
                Log.WriteEvent(string.Format("SpecialKeys: Could not grab key {0} (maybe another application has grabbed this key)", keycode));
        }

        private void UngrabKey(Gdk.Window root, int keycode, Gdk.ModifierType[] modemask)
        {
            IntPtr xid = NativeLinux.gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = NativeLinux.gdk_x11_get_default_xdisplay();

            NativeLinux.gdk_error_trap_push();

            foreach(var mmask in modemask)
            {
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, mmask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | mmask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, mmask | Gdk.ModifierType.LockMask, xid);
                NativeLinux.XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | mmask | Gdk.ModifierType.LockMask, xid);
            }

            NativeLinux.gdk_flush();

            if(NativeLinux.gdk_error_trap_pop() != 0)
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
    }
}
#endif