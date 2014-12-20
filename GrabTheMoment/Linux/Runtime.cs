#if __MonoCS__
using System;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public sealed class Runtime
    {
        public static void SetProcessName(string Name)
        {
            try
            {
                unixSetProcessName(Name);
            }
            catch(Exception e)
            {
                Log.WriteEvent(string.Format(("Failed to set process name: {0}"), e.Message));
            }
        }

        [DllImport("libc")] // Linux
        private static extern int prctl(int option, byte[] arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);

        [DllImport("libc")] // BSD
        private static extern void setproctitle(byte[] fmt, byte[] str_arg);

        // This is from http://abock.org/2006/02/09/changing-process-name-in-mono/
        private static void unixSetProcessName(string Name)
        {
            try
            {
                if(prctl(15, Encoding.ASCII.GetBytes(Name + "\0"), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) != 0)
                    Log.WriteEvent("Error setting process name!");
            }
            catch(EntryPointNotFoundException)
            {
                try
                {
                    // Not every BSD has setproctitle
                    setproctitle(Encoding.ASCII.GetBytes("%s\0"), Encoding.ASCII.GetBytes(Name + "\0"));
                }
                catch(EntryPointNotFoundException)
                {

                }
            }
        }

        public static Version GetMonoVersion()
        {
            Type type = Type.GetType("Mono.Runtime");

            if (type != null)
            {
                MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

                if (displayName != null)
                {
                    string dn = displayName.Invoke(null, null).ToString();
                    return new Version(dn.Substring(0, dn.IndexOf(" ")));
                }
            }

            return new Version(0, 0, 0);
        }
    }
}
#endif