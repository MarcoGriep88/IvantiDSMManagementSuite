using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für LicenseInfo.xaml
    /// </summary>
    public partial class LicenseInfo : Window
    {
        public LicenseInfo()
        {
            InitializeComponent();
            StreamReader ldata = new StreamReader(Environment.CurrentDirectory.ToString() + "\\ldata.bin");
            string license = ldata.ReadToEnd();
            licenseText.AppendText(license);
            ldata.Close();
        }
    }
}
