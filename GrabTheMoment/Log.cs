using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace GrabTheMoment
{
    /* public */ static class Log
    {
        public class EmptyPathException : SystemException
        {
            public EmptyPathException(string message) : base(message) { }
        }

        private static string _path = String.Empty;

        public static string LogPath
        {
            set { _path = value; }
        }

        public static void WriteEvent(string Information, Exception e = null)
        {
#if DEBUG
            if (_path == String.Empty)
                throw new EmptyPathException(String.Format("{0}: Üres a path.", ClassAndMethodName()));

            string mappautvonal = Path.GetDirectoryName(_path);
            string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Ha nincs mappa, akkor létrehozzuk
            if ((mappautvonal.Length > 0) && (!Directory.Exists(mappautvonal)))
                Directory.CreateDirectory(mappautvonal);

            StreamWriter writer = new StreamWriter(_path, true, Encoding.Unicode);
            string sor = String.Empty;

            if (Information != String.Empty && e == null)
                sor = String.Format("[{0}] [{1}] INFO: {2}", CurrentDateTime, ClassAndMethodName(), Information.Replace(System.Environment.NewLine, " @ "));
            else if (Information != String.Empty && e != null)
                sor = String.Format("[{0}] [{1}] INFO: {2} | ERROR:{3}", CurrentDateTime, ClassAndMethodName(), Information.Replace(System.Environment.NewLine, " @ "), e.ToString().Replace(System.Environment.NewLine, " @ "));
            else if (Information == String.Empty && e != null)
                sor = String.Format("[{0}] [{1}] ERROR: {2}", CurrentDateTime, ClassAndMethodName(), e.ToString().Replace(System.Environment.NewLine, " @ "));

            writer.WriteLine(sor);
            writer.Close();
#endif
        }

        // Ezzel meg lehet tudni, az adott osztalyt/metodust, ahonnan a WriteEvent-et hívták
        private static string ClassAndMethodName()
        {
            MethodBase method = new StackTrace().GetFrame(2).GetMethod();
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            return String.Format("{0}.{1}", className, methodName);
        }
    }
}
