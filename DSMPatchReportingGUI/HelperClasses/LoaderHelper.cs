using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI
{
    public static class LoaderHelper
    {
        private static Process[] pids;
        public static void LoaderKill()
        {
            try
            {
                foreach (var p in pids)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }

                }
            }
            catch { }
        }

        public static void LoaderStart()
        {
            try
            {
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\Loader.exe");
                var processes = Process.GetProcessesByName("Loader");
                pids = processes;
            }
            catch { }

        }
    }
}
