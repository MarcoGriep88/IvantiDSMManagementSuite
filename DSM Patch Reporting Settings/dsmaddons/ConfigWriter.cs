using DSMAddons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DSMAddons
{
    public class ConfigWriter : IConfigWriter
    {
        private const string _configDir = @"C:\Program Files (x86)\DSMPatchReporting";
        private readonly string _configFile = _configDir + "\\config.tsx";

        //Member Variable
        private Setting _defaultSettings = null;

        private String _lastError = String.Empty;
        private String LastError { get { return _lastError; } }

        public Setting DefaultSettings { get { return _defaultSettings; } }

        public ConfigWriter()
        {
            _defaultSettings = new Setting()
            {
                BLS = "localhost:8080",
                BLSUser = "domain\\username",
                BLSPassword = "12345",
                API = "https://localhost:8081",
                Context = "emdb:\\rootDSE\\Managed Users & Computers\\*",
                Location = "Default Location",
                Username = "ReportMaster",
                Password = "!Start01"
            };
        }

        public virtual Setting ReadConfig()
        {
            Setting setting = _defaultSettings;
            if (File.Exists(_configFile))
                setting = ParseFile(setting);

            return setting;
        }

        private Setting ParseFile(Setting setting)
        {
            using (StreamReader reader = new StreamReader(_configFile))
            {
                while (reader.Peek() >= 0)
                {
                    string ln = reader.ReadLine();
                    ParseLine(ln, ref setting);
                }
            }

            return setting;
        }

        public virtual void ParseLine(string ln, ref Setting setting)
        {
            setting.BLS = (ln.StartsWith("BLS=")) ? ln.Replace("BLS=", "") : setting.BLS;
            setting.BLSUser = (ln.StartsWith("BLSUser=")) ? ln.Replace("BLSUser=", "") : setting.BLSUser;
            setting.BLSPassword = (ln.StartsWith("BLSPassword=")) ? ln.Replace("BLSPassword=", "") : setting.BLSPassword;
            setting.API = (ln.StartsWith("API=")) ? ln.Replace("API=", "") : setting.API;
            setting.Context = (ln.StartsWith("Context=")) ? ln.Replace("Context=", "") : setting.Context;
            setting.Location = (ln.StartsWith("Location=")) ? ln.Replace("Location=", "") : setting.Location;
            setting.Username = (ln.StartsWith("Username=")) ? ln.Replace("Username=", "") : setting.Username;
            setting.Password = (ln.StartsWith("Password=")) ? ln.Replace("Password=", "") : setting.Password;
        }

        public virtual void WriteConfig(Setting setting)
        {
            try
            {
                PrepareDirectories();
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return;
            }

            try
            {
                StreamWriter writer = File.CreateText(@"C:\Program Files (x86)\DSMPatchReporting\config.tsx");
                writer.WriteLine("BLS=" + setting.BLS);
                writer.WriteLine("BLSUser=" + setting.BLSUser);
                writer.WriteLine("BLSPassword=" + setting.BLSPassword);
                writer.WriteLine("API=" + setting.API);
                writer.WriteLine("Context=" + setting.Context);
                writer.WriteLine("Location=" + setting.Location);
                writer.WriteLine("Username=" + setting.Username);
                writer.WriteLine("Password=" + setting.Password);
                writer.Close();
            }
            catch (Exception ex) {
                _lastError = ex.Message;
            }

        }

        private void PrepareDirectories()
        {
            if (!Directory.Exists(_configDir))
                Directory.CreateDirectory(_configDir);

            if (File.Exists(_configFile))
                File.Delete(_configFile);
        }
    }
}
