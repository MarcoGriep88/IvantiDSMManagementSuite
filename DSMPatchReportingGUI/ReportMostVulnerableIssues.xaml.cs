using System;
using System.Collections.Generic;
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
using static DSMPatchReportingGUI.Models.ExportDataType;
using DSMPatchReportingGUI.Models;

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für ReportMostVulnerableIssues.xaml
    /// </summary>
    public partial class ReportMostVulnerableIssues : Window
    {
        private DataFilter _dataFilter = null;
        public DataFilter DataFilter { get { return _dataFilter; } }

        public ExportDataType.DATA_TYPE DataType { get; set; }

        public ReportMostVulnerableIssues()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _dataFilter = new DataFilter()
            {
                ComputerName = (checkBox.IsChecked == true && ComputerNameTextbox.Text.Length > 0) ? ComputerNameTextbox.Text : "",
                DataSetCount = 0
            };
            CheckDataType();

            this.DialogResult = true;
            this.Close();
        }

        private void CheckDataType()
        {
            switch (this.DataTypeComboBox.SelectedIndex)
            {
                case 0:
                    this.DataType = DATA_TYPE.CSV;
                    break;
                case 1:
                    this.DataType = DATA_TYPE.XLS;
                    break;
                case 2:
                    this.DataType = DATA_TYPE.HTML;
                    break;
                case 3:
                    this.DataType = DATA_TYPE.XML;
                    break;
                case 4:
                    this.DataType = DATA_TYPE.PDF;
                    break;
                default:
                    this.DataType = DATA_TYPE.CSV;
                    break;
            }
        }
    }
}
