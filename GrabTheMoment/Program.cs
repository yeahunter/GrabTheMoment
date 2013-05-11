using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GrabTheMoment
{
    static class Program
    {
        static Form1 windowsform;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            windowsform = new Form1();
            InterceptKeys.Hook(windowsform);
            Application.Run(windowsform);
        }
    }
}
