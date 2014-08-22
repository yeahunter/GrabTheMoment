#if __MonoCS__
using System;
using System.Collections;

namespace GrabTheMoment.Linux
{
    public class HookKey
    {
        public Gdk.ModifierType ModeMask { get; private set; }
        public int Key { get; private set; }

        public HookKey(int key, Gdk.ModifierType modemask)
        {
            Key = key;
            ModeMask = modemask;
        }
    }
}
#endif