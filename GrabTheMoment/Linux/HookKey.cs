#if __MonoCS__
using System;

namespace GrabTheMoment.Linux
{
    public class HookKey
    {
        public Gdk.ModifierType ModeMask = Gdk.ModifierType.None;
        public SpecialKey SKey { get; private set; }
        public SpecialKeyPressedHandler SKPHMethod;
        public int Key { get; private set; }

        public HookKey(int key, SpecialKey skey)
        {
            Key = key;
            SKey = skey;
        }
    }
}
#endif