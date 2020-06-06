using DSMPatchReportingGUI.Controller;
using DSMPatchReportingGUI.Interfaces;
using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DSMPatchReportingGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataController dataController;
        private Connection _connection = null;
        private string _latestDate = "";
        private readonly static string _CacheDirectory = @"C:\temp\DSMPatchReporting";
        private static string _lastServerHistoryCacheFile = _CacheDirectory + "\\lastServer.cache";

        private String _version = "2019-11";

        private List<String> ComputerList = null;
        private List<String> DateList = null;
        private DataFilter DataFilter = null;
        private bool alreadyLoggedIn = false;

        private int _progressBarValue = 0;

        System.Windows.Threading.DispatcherTimer updateProgressBarTimer = new System.Windows.Threading.DispatcherTimer();
        
        public MainWindow(Connection connection = null, String Computer = "")
        {
            InitializeComponent();
            MenuReportingCategory.IsEnabled = false;
            this._connection = connection;
            dataController = new APIDataController();
            updateProgressBarTimer.Tick += new EventHandler(UpdateProcessBar);
            updateProgressBarTimer.Interval = new TimeSpan(0, 0, 1);
            updateProgressBarTimer.Start();
            DateList = new List<string>();

#if LITE
            this.Title = "DSM Management Suite - LITE Edition";
            _version += " LITE";
            CummulativeReport.IsEnabled = false;
#else
            LiteVersionInfo.Visibility = Visibility.Hidden;
#endif

            if (_connection != null)
            {
                if (Computer.Length > 0)
                {
                    DataFilter = new DataFilter()
                    {
                        ComputerName = Computer
                    };
                    this.txtFilterComputer.Text = Computer;
                }
                LoginCoroutine();
            }
            else
            {
                ShowLogin();
            }    
        }

        private void UpdateProcessBar(object sender, EventArgs e)
        {
            this.ProcessStatusBar.Value = _progressBarValue;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ShowLogin();
        }

        private void ShowLogin()
        {
            LoginDialog loginDialog = new LoginDialog();

            if (loginDialog.ShowDialog().HasValue == true)
                Login(loginDialog);
        }

        private void Login(LoginDialog loginDialog)
        {
            if (!alreadyLoggedIn)
            {
                _connection = loginDialog.GetAuthInfo();

                if (_connection == null)
                    return;
                LoginCoroutine();
            }
            else
            {
                MessageBox.Show("Sie sind bereits angemeldet");
            }
        }

        private void LoginCoroutine()
        {
            LoaderHelper.LoaderStart();

            if (_connection.BaseURL.Length > 0 && _connection.Username.Length > 0 && _connection.Password.Length > 0)
            {
                _connection.Token = dataController.Login(_connection.BaseURL,
                new UserForLoginDto()
                {
                    Username = _connection.Username,
                    Password = _connection.Password
                });

                SaveLastLoginServer();

                if (_connection.Token == null)
                    MessageBox.Show("Login fehlgeschlagen");
                else
                {
                    MenuReportingCategory.IsEnabled = true;
                    ManageUsersMenuButton.IsEnabled = true;
                    RefreshData();
                } 
            }
        }

        private void SaveLastLoginServer()
        {
            try
            {
                if (!Directory.Exists(_CacheDirectory))
                    Directory.CreateDirectory(_CacheDirectory);

                if (File.Exists(_lastServerHistoryCacheFile))
                    File.Delete(_lastServerHistoryCacheFile);

                StreamWriter saveLastConnection = File.CreateText(_lastServerHistoryCacheFile);
                saveLastConnection.WriteLine(this._connection.BaseURL);
                saveLastConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RefreshData()
        {
            alreadyLoggedIn = true;
            LoginMenuButton.IsEnabled = false;
            var data = dataController.GetDateOpenClosedStats(_connection.BaseURL, _connection.Token, DataFilter);
            var issues = dataController.GetMostSecurityIssues(_connection.BaseURL, _connection.Token, DataFilter);
            DisplayTrendLine(data);
            DisplayOpenClosedPieChart(data);
            DisplayMostVulnerableIssues(issues);

            LoaderHelper.LoaderKill();
            
        }

        private void DisplayMostVulnerableIssues(List<PatchCountOfComplianceDto> issues)
        {
            List<KeyValuePair<string, double>> keyValuePairs = new List<KeyValuePair<string, double>>();
            foreach (var d in issues)
            {
                keyValuePairs.Add(new KeyValuePair<string, double>(d.Patch, d.Count));
            }

            int c = 10;
#if LITE
            c = 5;
#endif
            if (keyValuePairs.Count < c) c = keyValuePairs.Count;

            ((ColumnSeries)mcChart_ein.Series[0]).ItemsSource = keyValuePairs.GetRange(0, c);
        }

        /// <summary>
        /// Zeigt Pie Chart an
        /// </summary>
        /// <param name="data"></param>
        private void DisplayOpenClosedPieChart(List<DateOpenClosedStatDto> data)
        {
            List<KeyValuePair<string, int>> openclosed = new List<KeyValuePair<string, int>>();

            openclosed.Add(
                new KeyValuePair<string, int>(
                    "Geschlossen",
                    data.Where(d => d.Date == _latestDate).Sum(x => x.Fixed)
                ));

            openclosed.Add(
               new KeyValuePair<string, int>(
                    "Nicht Geschlossenen",
                    data.Where(d => d.Date == _latestDate).Sum(x => x.NotFixed)
                ));

            openclosed.Add(
                new KeyValuePair<string, int>(
                    "Nicht anwendbar",
                    data.Where(d => d.Date == _latestDate).Sum(x => x.NotApplicable)
                ));


            ((PieSeries)mcChart.Series[0]).ItemsSource = openclosed;
        }

        private void DisplayTrendLine(List<DateOpenClosedStatDto> data)
        {
            DateList.Clear();
            List<KeyValuePair<string, double>> keyValuePairs = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> trend = new List<KeyValuePair<string, double>>();
#if LITE
            if (data.Count > 5)
            {
                int index = data.Count - 5;
                var tmp = data.GetRange(index, 5);
                data = tmp;
            }
#endif
            int counter = 1;
            int sumNotFixed = 0;
            float weight = 1f;
            foreach (var d in data)
            {
                keyValuePairs.Add(new KeyValuePair<string, double>(d.Date, d.NotFixed));
                sumNotFixed += d.NotFixed;
                trend.Add(new KeyValuePair<string, double>(d.Date, (sumNotFixed / (counter*weight))));
                DateList.Add(d.Date);
                this._latestDate = d.Date;
                counter++;
            }

            ((LineSeries)mcChart_ein_Copy.Series[0]).ItemsSource = keyValuePairs;
            ((LineSeries)mcChart_ein_Copy.Series[1]).ItemsSource = trend;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFilter();
        }

        private void RefreshFilter()
        {
            if (txtFilterComputer.Text.Length > 0)
            {
                if (DataFilter != null)
                {
                    DataFilter.ComputerName = txtFilterComputer.Text;
                }
                else
                {
                    DataFilter = new DataFilter()
                    {
                        ComputerName = txtFilterComputer.Text
                    };
                }
            }
            else
            {
                DataFilter = null;
            }
            RefreshData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComputerList == null)
                ComputerList = dataController.GetComputers(_connection.BaseURL, _connection.Token);

            SelectComputer selectComputer = new SelectComputer(ComputerList);
            selectComputer.ShowDialog();
            this.txtFilterComputer.Text = selectComputer.SelectedComputer;
            RefreshFilter();
            RefreshData();
        }

        private void LastestSecurityIssuesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (DateList.Count > 1)
            {
                DateList.RemoveAt(DateList.Count - 1);                      //Den neusten Eintrag entfernen
                var dates = DateList.OrderByDescending(d => d).ToList();    //Von neusten zu am längsten her umsortieren

                ReportLatestSecurityIssues reportLatestSecurityIssues = new ReportLatestSecurityIssues(dates);
                reportLatestSecurityIssues.ShowDialog();

                if (reportLatestSecurityIssues.DialogResult == true)
                {
                    LoaderHelper.LoaderStart();

                    ProcessStatusBar.Visibility = Visibility.Visible;
                    ProcessStatusText.Content = "Erstelle Report: Neuste Sicherheitslücken";
                    ProcessStatusBar.Value = 10;

                    if (DataFilter == null)
                        DataFilter = new DataFilter();
                    DataFilter.DataSetCount = reportLatestSecurityIssues.DatesBack;

                    ExportDataType.DATA_TYPE dataType = reportLatestSecurityIssues.DataType;

                    (new Thread(() => {
                        var issues = dataController.GetLatestSecurityIssues(_connection.BaseURL, _connection.Token, DataFilter);
                        _progressBarValue = 25;
                        ReportController.CreateLatestReport(dataType, issues);
                    })).Start();
                }
            }
            else
            {
                MessageBox.Show("Noch nicht genug Daten gesammt um Report zu erstellen.");
            }
        }

        private void MostVulerableIssuesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ReportMostVulnerableIssues reportMostVulnerableIssues = new ReportMostVulnerableIssues();            
            reportMostVulnerableIssues.ShowDialog();

            if (reportMostVulnerableIssues.DialogResult.Value == true)
            {
                LoaderHelper.LoaderStart();

                ProcessStatusBar.Visibility = Visibility.Visible;
                ProcessStatusText.Content = "Erstelle Report: Meisten Sicherheitslücken";

                if (reportMostVulnerableIssues.DataFilter == null)
                    DataFilter = new DataFilter();
                else
                    DataFilter = reportMostVulnerableIssues.DataFilter;

                _progressBarValue = 25;

                (new Thread(() => {
                    var issues = dataController.GetMostSecurityIssues(_connection.BaseURL, _connection.Token, DataFilter);
                    _progressBarValue = 25;
                    ReportController.CreateVulnReport(reportMostVulnerableIssues.DataType, issues);
                    _progressBarValue = 100;
                })).Start();
                ProcessStatusText.Content = "Beendet";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ReportController.DataDirectory);
        }

        private void ManageUsersMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersDialog manageUsersDialog = new ManageUsersDialog(ref _connection);
            manageUsersDialog.ShowDialog();
        }

        private void ReportNachTag_Status_Click_1(object sender, RoutedEventArgs e)
        {
            ReportPatchesByDate reportPatchStatus = new ReportPatchesByDate();
            reportPatchStatus.ShowDialog();

            if (reportPatchStatus.DialogResult.Value == true)
            {
                LoaderHelper.LoaderStart();

                ProcessStatusBar.Visibility = Visibility.Visible;
                ProcessStatusText.Content = "Erstelle Report: Report Patches nach Tag";

                if (reportPatchStatus.DataFilter == null)
                    DataFilter = new DataFilter();
                else
                    DataFilter = reportPatchStatus.DataFilter;

                _progressBarValue = 25;

                (new Thread(() => {
                    var issues = dataController.GetPatchDataByDate(_connection.BaseURL, _connection.Token, DataFilter);
                    _progressBarValue = 25;
                    ReportController.CreateReportByDateReport(reportPatchStatus.DataType, issues);
                    _progressBarValue = 100;
                })).Start();
                ProcessStatusText.Content = "Beendet";
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
#if DEMO
            Process.Start("http://demo.dsm-management-suite.de");
#endif
            if (!File.Exists(Environment.CurrentDirectory.ToString() + "\\dashboard.url"))
            {
                MessageBox.Show("dashboard.url Datei existiert nicht im Programmverzeichnis. Bitte erstellen Sie manuell eine Verknüpfung zum Web Dashboard." + Environment.NewLine +
                    "Rechtsklick -> Verknüpfung -> URL von Ihrem Dashboard eingeben -> Weiter -> Dateiname: dashboard.url");
            }
            else
            {
                Process.Start(Environment.CurrentDirectory.ToString() + "\\dashboard.url");
            }
            
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DateList.RemoveAt(DateList.Count - 1);                      //Den neusten Eintrag entfernen
            var dates = DateList.OrderByDescending(d => d).ToList();    //Von neusten zu am längsten her umsortieren

            ReportComplianceStatus reportPatchStatus = new ReportComplianceStatus(dates);
            reportPatchStatus.ShowDialog();

            if (reportPatchStatus.DialogResult.Value == true)
            {
                LoaderHelper.LoaderStart();

                ProcessStatusBar.Visibility = Visibility.Visible;
                ProcessStatusText.Content = "Erstelle Report: Compliance Report";

                if (reportPatchStatus.DataFilter == null)
                    DataFilter = new DataFilter();
                else
                    DataFilter = reportPatchStatus.DataFilter;

                _progressBarValue = 25;

                (new Thread(() => {
                    var issues = dataController.GetFixedInPercent(_connection.BaseURL, _connection.Token, DataFilter);
                    _progressBarValue = 25;
                    ReportController.CreateComplianceStatusReport(reportPatchStatus.DataType, issues);
                    _progressBarValue = 100;
                })).Start();
                ProcessStatusText.Content = "Beendet";
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            LoaderHelper.LoaderStart();

            ProcessStatusBar.Visibility = Visibility.Visible;
            ProcessStatusText.Content = "Erstelle Report: Zusammenfassender Report";

            _progressBarValue = 25;

            var dates = DateList.OrderByDescending(d => d).ToList();
            DataFilter = new DataFilter();

            DataFilter.SpecificDate = DateTime.Parse(dates.First());

            (new Thread(() => {
                var data = dataController.GetDateOpenClosedStats(_connection.BaseURL, _connection.Token, null);
                var issues = dataController.GetMostSecurityIssues(_connection.BaseURL, _connection.Token, DataFilter);
                var patches = dataController.GetPatchDataByDate(_connection.BaseURL, _connection.Token, DataFilter);
                _progressBarValue = 25;
                ReportController.CreateMasterStatusReport(ExportDataType.DATA_TYPE.HTML, data, issues, patches);
                _progressBarValue = 100;
            })).Start();
            ProcessStatusText.Content = "Beendet";
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            LicenseInfo license = new LicenseInfo();
            license.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_version, "Version Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.dsm-management-suite.de/index.php/portal/forum");
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.dsm-management-suite.de");
        }
    }
}
