using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace GrabTheMoment
{
    static class Program
    {
        static Main windowsform;
        static string AppGUID = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
        static bool CanIRun = false;

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
                    CanIRun = true;
                
            }
            catch (Exception)
            {
                CanIRun = true;
            }

            if (CanIRun)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                windowsform = new Main();
                InterceptKeys.Hook(windowsform);
                Application.Run(windowsform);
            }
        }
    }
}
