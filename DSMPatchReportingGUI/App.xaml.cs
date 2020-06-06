using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    { 
        void App_Startup(object sender, StartupEventArgs e)
        {
            Connection connection = null;
            String Computer = String.Empty;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i].StartsWith("/API=")) {
                    connection = new Connection();
                    connection.BaseURL = e.Args[i].Replace("/API=", "");
                }
                if (e.Args[i].StartsWith("/USER=")) connection.Username = e.Args[i].Replace("/USER=", "");
                if (e.Args[i].StartsWith("/PWD=")) connection.Password = e.Args[i].Replace("/PWD=", "");
                if (e.Args[i].StartsWith("/COMPUTER=")) Computer = e.Args[i].Replace("/COMPUTER=", "");
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow(connection, Computer);
            mainWindow.Show();
        }
    }
}
