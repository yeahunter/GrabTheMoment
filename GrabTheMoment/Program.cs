using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace GrabTheMoment
{
    static class Program
    {
        static Form1 windowsform;
        static string AppGUID = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
        static bool futhatoke = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Mutex mutex = new Mutex(false, "Local\\" + AppGUID);
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Már fut a program!");
                    mutex.ReleaseMutex();
                    mutex = null;
                    return;
                }
                else
                {
                    futhatoke = true;
                }
                
            }
            catch (Exception)
            {
                futhatoke = true;
            }

            if (futhatoke)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                windowsform = new Form1();
                InterceptKeys.Hook(windowsform);
                Application.Run(windowsform);
            }
        }
    }
}
