using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GrabTheMoment
{
    class Log
    {
        public void WriteEvent(string Information)
        {

            try
            {
#if DEBUG
                StreamWriter writer = new StreamWriter(string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM")), true, Encoding.Unicode);
                string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine("[" + CurrentDateTime + "] INFO: " + Information);
                writer.WriteLine("");
                writer.Close();
#endif
            }
            catch { }
        }
        public void WriteExceptionEvent(Exception e, string AdditionalInformation)
        {
            try
            {
#if DEBUG
                StreamWriter writer = new StreamWriter(string.Format("DEBUG-{0}.log", DateTime.Now.ToString("yyyy-MM")), true, Encoding.Unicode);
                string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine("[" + CurrentDateTime + "] ERROR: " + AdditionalInformation);
                writer.WriteLine(e.ToString());
                writer.WriteLine("");
                writer.Close();
#endif
            }
            catch { }
        }
    }
}
