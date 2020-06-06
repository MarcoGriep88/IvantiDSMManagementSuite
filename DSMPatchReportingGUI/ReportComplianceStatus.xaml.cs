using DSMPatchReportingGUI.Models;
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

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für ReportComplianceStatus.xaml
    /// </summary>
    public partial class ReportComplianceStatus : Window
    {
        private DataFilter _dataFilter = null;
        public DataFilter DataFilter { get { return _dataFilter; } }

        private List<String> Dates { get; set; }
        public ExportDataType.DATA_TYPE DataType { get; set; }

        public ReportComplianceStatus(List<string> dateList)
        {
            InitializeComponent();
            this.Dates = dateList;
            InitComboBox();
        }

        private void InitComboBox()
        {
            if (Dates == null)
            {
                return;
            }

            foreach (var d in Dates)
            {
                this.DatesComboBox.Items.Add(d);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckDataType();

            _dataFilter = new DataFilter();

            if (this.checkBox.IsChecked==true)
            {
                _dataFilter.ComputerName = this.ComputerNameTextbox.Text;
            }
            else
            {
                _dataFilter.ComputerName = "";
            }

            var date = this.DatesComboBox.Items[this.DatesComboBox.SelectedIndex].ToString();

            _dataFilter.SpecificDate = DateTime.Parse(date);

            //TODO date to DateTime
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

        private void SelectionChagned(object sender, SelectionChangedEventArgs e)
        {
#if LITE
            try
            {
                this.FeatureNotAvailableInfo.Visibility = (this.DataTypeComboBox.SelectedIndex > 1) ? Visibility.Visible : Visibility.Hidden;
                this.CreateReportButton.IsEnabled = (this.DataTypeComboBox.SelectedIndex <= 1);
            }
            catch { }
#endif
        }
    }
}
