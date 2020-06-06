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
    /// Interaktionslogik für ManageUsersDialog.xaml
    /// </summary>
    public partial class ManageUsersDialog : Window
    {
        List<String> _userList = null;
        private string _selectedUser = String.Empty;
        private Connection _connection = null;

        IDataController dataController = new APIDataController();

        public ManageUsersDialog(ref Connection connection)
        {
            _connection = connection;
            InitializeComponent();
            RefreshUserData();

#if DEMO
            this.NewButton.IsEnabled = false;
            this.SaveButton.IsEnabled = false;
            this.DeleteButton.IsEnabled = false;
            this.DEMO_INFO_GROUP.Visibility = Visibility.Visible;
#elif LITE
            this.NewButton.IsEnabled = false;
            this.DEMO_INFO_GROUP.Visibility = Visibility.Visible;
            this.DSM_INFO_LABEL.Content = "In LITE Version ist es nicht möglich mehr als 1 Account anzulegen";
#else
            this.DEMO_INFO_GROUP.Visibility = Visibility.Hidden;
#endif
        }

        private void RefreshUserData()
        {
            _userList = dataController.GetUsers(_connection.BaseURL, _connection.Token);
            RefreshUserList();
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UserListBox.SelectedIndex > -1)
            {
                this.UserConfigurationGrid.IsEnabled = true;
                _selectedUser = _userList[this.UserListBox.SelectedIndex];
                this.UserNameLabel.Content = _selectedUser;
            }
            else
            {
                this.UserConfigurationGrid.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser.Length > 0)
            {
                if (MessageBox.Show("Sicher das Sie den Benutzer " + _selectedUser + " löschen möchten?", 
                    "Frage", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    DeleteUser();
                }
            }
        }

        private void DeleteUser()
        {
            if (_connection == null)
                return;

            if (!dataController.DeleteUser(_connection.BaseURL, _connection.Token, _selectedUser))
            {
                MessageBox.Show("Fehler beim löschen des Benutzers: " + _selectedUser + "."
                    + Environment.NewLine + dataController.GetLastError());
            }

            RefreshUserData();
        }

        private void RefreshUserList()
        {
            this.UserListBox.Items.Clear();

            if (_userList == null)
                return;

            foreach (var s in _userList)
            {
                this.UserListBox.Items.Add(s);
            }
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
#if !LITE
            NewUserDialog newUserDialog = new NewUserDialog(ref _connection);
            newUserDialog.ShowDialog();
            RefreshUserData();
#endif
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!dataController.ChangePassword(_connection.BaseURL, _connection.Token, new UserForRegisterDto()
            {
                Username = _selectedUser,
                Password = this.pwdBox.Password
            }, this.pwdBox.Password)) {
                MessageBox.Show("Fehler beim Ändern des Passworts für Benutzer: " + _selectedUser);
            }
            else
            {
                MessageBox.Show("Erfolgreich geändert");
                this.checkBox.IsChecked = false;
                this.pwdBox.Password = "";
            }
        }
    }
}
