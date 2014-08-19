#if __MonoCS__
using System;

namespace GrabTheMoment.Linux
{
    [Flags]
    public enum XModMask
    {
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
}
#endif