using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GrabTheMoment
{
    public static class Log
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
                throw new EmptyPathException(String.Format("{0}: Üres a path.", String.Empty /* Itt majd valami jo cucc lesz */));

            string mappautvonal = Path.GetDirectoryName(_path);
            string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if ((mappautvonal.Length > 0) && (!Directory.Exists(mappautvonal)))
                Directory.CreateDirectory(mappautvonal);

            StreamWriter writer = new StreamWriter(_path, true, Encoding.Unicode);
            string sor = String.Empty;

            if (Information != String.Empty && e == null)
                sor = String.Format("[{0}] [{1}] INFO: {2}", CurrentDateTime, String.Empty /* Itt majd valami jo cucc lesz */, Information.Replace(System.Environment.NewLine, " @ "));
            else if (Information != String.Empty && e != null)
                sor = String.Format("[{0}] [{1}] INFO: {2} | ERROR:{3}", CurrentDateTime, String.Empty /* Itt majd valami jo cucc lesz */, Information.Replace(System.Environment.NewLine, " @ "), e.ToString().Replace(System.Environment.NewLine, " @ "));
            else if (Information == String.Empty && e != null)
                sor = String.Format("[{0}] [{1}] ERROR: {2}", CurrentDateTime, String.Empty /* Itt majd valami jo cucc lesz */, e.ToString().Replace(System.Environment.NewLine, " @ "));

            writer.WriteLine(sor);
            writer.Close();
#endif
        }
    }
}
