using DSMAddons;
using DSMPatchReportingGUI.Controller;
using DSMPatchReportingGUI.Interfaces;
using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSM_Patch_Reporting_Settings
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConfigWriter configWriter = null;

        public MainWindow()
        {
            InitializeComponent();
            configWriter = new EncryptedConfigWriter();
            ReadExistingSettings();
        }

        private void ReadExistingSettings()
        {
            var settings = configWriter.ReadConfig();
            txtBLS.Text = settings.BLS;
            txtBLSUser.Text = settings.BLSUser;
            txtBLSPassword.Password = settings.BLSPassword;
            txtUser.Text = settings.Username;
            txtPassword.Password = settings.Password;
            txtLocation.Text = settings.Location;
            txtContext.Text = settings.Context;
            txtAPI.Text = settings.API;
        }

        private void SaveSettings_Clicked(object sender, RoutedEventArgs e)
        {
            configWriter.WriteConfig(new Setting()
            {
                BLS = txtBLS.Text,
                BLSUser = txtBLSUser.Text,
                BLSPassword = txtBLSPassword.Password,
                Context = txtContext.Text,
                API = txtAPI.Text,
                Location = txtLocation.Text,
                Username = txtUser.Text,
                Password = txtPassword.Password
            });

            this.Close();
        }

        private void ExitApplication_Clicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting()
            {
                BLS = txtBLS.Text,
                BLSUser = txtBLSUser.Text,
                BLSPassword = txtBLSPassword.Password,
                Context = txtContext.Text,
                API = txtAPI.Text,
                Location = txtLocation.Text,
                Username = txtUser.Text,
                Password = txtPassword.Password
            };
            if (!DSMUtitities.CheckDSMAccess(setting))
            {
                MessageBox.Show("Ooops. Die Anmeldung am BLS ist fehlgeschlagen");
                return;
            }

            // TODO: Check API Access
            IDataController dataController = new APIDataController();

            JWT token = dataController.Login(setting.API,
            new UserForLoginDto()
            {
                Username = setting.Username,
                Password = setting.Password
            });

            if (token == null)
                MessageBox.Show("Login bei der API ist fehlgeschlagen");
            else
                MessageBox.Show("Verbindungsinformationen sind korrekt");
        }
    }
}
