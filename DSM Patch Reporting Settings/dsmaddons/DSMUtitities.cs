using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMAddons
{
    public static class DSMUtitities
    {
        public static bool CheckDSMAccess(Setting setting)
        {
            if (!Directory.Exists(@"C:\ProgramData\EasyAdminMaster\Data\cache"))
            {
                Directory.CreateDirectory(@"C:\ProgramData\EasyAdminMaster\Data\cache");
            }
            

            if (setting == null)
                return false;

            StartTool(Environment.CurrentDirectory.ToString() + "\\ext\\detsites.exe",
                "-argServer " + setting.BLS + 
                " -argUser " + setting.BLSUser + 
                " -argPassword " + setting.BLSPassword);

            if (!File.Exists(@"C:\ProgramData\EasyAdminMaster\Data\cache\sites.cache"))
                return false;

            return true;
        }

        public static void StartTool(String path, String parameters, bool wait = true)
        {
            if (!File.Exists(path))
                return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.CreateNoWindow = true;
            psi.Arguments = parameters;
            psi.FileName = path;

            Process p = new Process();
            p.StartInfo = psi;
            p.Start();

            if (wait) p.WaitForExit();
        }
    }
}
