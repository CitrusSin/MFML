using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MFML.UI;
using MFML.Core;

namespace MFML
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LauncherMain Instance = LauncherMain.CreateInstance();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(Instance));
        }
    }
}
