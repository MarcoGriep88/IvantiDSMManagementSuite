using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DSMPatchReportingGUI.Controller
{
    public class ReportWriter : StreamWriter
    {
        internal string _FilePath = String.Empty;
        public string FilePath { get { return _FilePath; } }

        public ReportWriter(string fileName):base(fileName)
        {
            _FilePath = fileName;
        }

        public virtual void StartFile()
        {
            try
            {
                Process.Start(_FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
