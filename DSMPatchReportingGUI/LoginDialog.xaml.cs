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
using DSMPatchReportingGUI.Models;

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        private Connection _connection { get; set; }

        private readonly static string _CacheDirectory = @"C:\temp\DSMPatchReporting";
        private static string _lastServerHistoryCacheFile = _CacheDirectory + "\\lastServer.cache";

        public LoginDialog()
        {
            InitializeComponent();

#if DEMO
            this.txtAPI.Text = "http://dsmapi.marcogriep.de";
            this.txtAPI.IsEnabled = false;
            this.txtAPI.IsReadOnly = true;
#else
            ReadLastConnectionInfo();
#endif
        }

        private void ReadLastConnectionInfo()
        {
            if (!Directory.Exists(_CacheDirectory))
                Directory.CreateDirectory(_CacheDirectory);

            if (File.Exists(_lastServerHistoryCacheFile))
            {
                using (StreamReader reader = new StreamReader(_lastServerHistoryCacheFile))
                {
                    string ln = reader.ReadLine();
                    this.txtAPI.Text = ln;
                }
            }
        }

        internal Connection GetAuthInfo()
        {
            return _connection;       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnClickEvent();
        }

        private void BtnClickEvent()
        {
            this._connection = new Connection()
            {
                BaseURL = txtAPI.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Password
            };
            this.DialogResult = true;
            this.Close();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnClickEvent();
        }
    }
}
