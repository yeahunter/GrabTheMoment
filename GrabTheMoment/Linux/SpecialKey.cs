#if __MonoCS__
using System;

namespace GrabTheMoment.Linux
{
    public enum SpecialKey
    {
        // TODO: Print gombra átrakni.
        Print = Gdk.Key.Page_Up,
        AltPrint = Gdk.Key.Page_Down
    };
}
#endif