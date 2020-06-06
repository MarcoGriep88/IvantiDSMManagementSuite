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

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für SelectComputer.xaml
    /// </summary>
    public partial class SelectComputer : Window
    {
        public String SelectedComputer { get; private set; }

        public SelectComputer(List<String> computers)
        {
            InitializeComponent();
            foreach(var computer in computers.OrderBy(n => n))
            {
                this.computerList.Items.Add(computer);
            }
        }

        private void ComputerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedComputer = computerList.SelectedItem.ToString();
        }

        private void MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            this.SelectedComputer = computerList.SelectedItem.ToString();
            this.Close();
        }
    }
}
