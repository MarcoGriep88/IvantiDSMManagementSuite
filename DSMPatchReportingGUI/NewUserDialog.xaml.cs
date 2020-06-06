using DSMPatchReportingGUI.Controller;
using DSMPatchReportingGUI.Interfaces;
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

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für NewUserDialog.xaml
    /// </summary>
    public partial class NewUserDialog : Window
    {
        private Connection _connection = null;

        public NewUserDialog(ref Connection connection)
        {
            InitializeComponent();
            _connection = connection;
        }

        /// <summary>
        /// Neuen Benutzer anlegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IDataController dataController = new APIDataController();

            if (CheckPasswordPolicy())
            {
                if (dataController.RegisterUser(_connection.BaseURL, _connection.Token, new UserForRegisterDto()
                {
                    Username = this.txtUsername.Text,
                    Password = this.txtPassword.Password
                }))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Fehler beim Registrieren des Benutzers." + Environment.NewLine +
                        dataController.GetLastError());
                }
            }
        }

        /// <summary>
        /// Prüft Passwort Richtlinie
        /// </summary>
        /// <returns></returns>
        private bool CheckPasswordPolicy()
        {
            if (this.txtPassword.Password != this.txtPassword_Copy.Password)
            {
                MessageBox.Show("Die Passwörter stimmen nicht miteinander überein");
                return false;
            }

            if (this.txtPassword.Password.Length < 8 || this.txtPassword.Password.Length > 45)
            {
                MessageBox.Show("Das Passwort muss zwischen 8 und 45 Zeichen lang sein");
                return false;
            }

            return true;
        }
    }
}
