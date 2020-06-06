using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;

namespace VisioDSMPlugin
{
    public partial class Ribbon1
    {
        private const string _configDir = @"C:\Program Files (x86)\DSMPatchReporting";

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            //ReloadConnectionSettings();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = _configDir + "\\DSM Patch Reporting Settings.exe";
            p.StartInfo.WorkingDirectory = _configDir;
            p.Start();
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Visio.Page visioPage =
                    Globals.ThisAddIn.Application.ActivePage;
            Microsoft.Office.Interop.Visio.Window visioWindow =
                    Globals.ThisAddIn.Application.ActiveWindow;

            try
            {
                if (visioWindow.Selection.Count == 0)
                {
                    MessageBox.Show("Please select a shape that has connections");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Please select a shape that has connections");
                return;
            }

            EncryptedConfigWriter encrypted = new EncryptedConfigWriter();
            Setting ConnectionSetting = encrypted.ReadConfig();
            
            Process p = new Process();
            p.StartInfo.FileName = _configDir + "\\Dashboard\\DSMPatchReportingGUI.exe";
            p.StartInfo.WorkingDirectory = _configDir + "\\Dashboard";
            p.StartInfo.LoadUserProfile = true;
            p.StartInfo.Arguments = "/API=" + ConnectionSetting.API + 
                " /USER=" + ConnectionSetting.Username + 
                " /PWD=" + ConnectionSetting.Password + 
                " /COMPUTER=" + visioWindow.Selection.PrimaryItem.Text;
            p.Start();
        }

        private void button5_Click(object sender, RibbonControlEventArgs e)
        {
            MessageBox.Show("Ok");
        }
    }

    public class Setting
    {
        public String BLS { get; set; }
        public String BLSUser { get; set; }
        public String BLSPassword { get; set; }
        public String Context { get; set; }
        public String API { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Location { get; set; }
    }

    public class EncryptedConfigWriter : ConfigWriter
    {
        private string secret = "mdePoxiblitashd90290";

        public EncryptedConfigWriter() : base() { }

        public override Setting ReadConfig()
        {
            Setting encryptedSettings = base.ReadConfig();

            if (encryptedSettings == null)
                return new Setting();

            Setting decryptedSettings = new Setting();
            DecryptSettings(encryptedSettings, decryptedSettings);
            return decryptedSettings;
        }

        public override void WriteConfig(Setting setting)
        {
            Setting encryptedSettings = new Setting();
            EncryptSettings(setting, encryptedSettings);

            base.WriteConfig(encryptedSettings);
        }

        private void EncryptSettings(Setting setting, Setting encryptedSettings)
        {
            encryptedSettings.BLS = Crypotography.EncryptStringAES(setting.BLS, secret);
            encryptedSettings.BLSUser = Crypotography.EncryptStringAES(setting.BLSUser, secret);
            encryptedSettings.BLSPassword = Crypotography.EncryptStringAES(setting.BLSPassword, secret);
            encryptedSettings.API = Crypotography.EncryptStringAES(setting.API, secret);
            encryptedSettings.Context = Crypotography.EncryptStringAES(setting.Context, secret);
            encryptedSettings.Location = Crypotography.EncryptStringAES(setting.Location, secret);
            encryptedSettings.Username = Crypotography.EncryptStringAES(setting.Username, secret);
            encryptedSettings.Password = Crypotography.EncryptStringAES(setting.Password, secret);
        }

        private void DecryptSettings(Setting setting, Setting decryptedSettings)
        {
            decryptedSettings.BLS = Crypotography.DecryptStringAES(setting.BLS, secret);
            decryptedSettings.BLSUser = Crypotography.DecryptStringAES(setting.BLSUser, secret);
            decryptedSettings.BLSPassword = Crypotography.DecryptStringAES(setting.BLSPassword, secret);
            decryptedSettings.API = Crypotography.DecryptStringAES(setting.API, secret);
            decryptedSettings.Context = Crypotography.DecryptStringAES(setting.Context, secret);
            decryptedSettings.Location = Crypotography.DecryptStringAES(setting.Location, secret);
            decryptedSettings.Username = Crypotography.DecryptStringAES(setting.Username, secret);
            decryptedSettings.Password = Crypotography.DecryptStringAES(setting.Password, secret);
        }
    }

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
            catch (Exception ex)
            {
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

    public interface IConfigWriter
    {
        void WriteConfig(Setting setting);
        Setting ReadConfig();
    }

    public class Crypotography
    {
        private static byte[] _salt = Encoding.ASCII.GetBytes("das80DSM2224asd48SPPSP42kbM7c5");

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAES(string cipherText, string sharedSecret)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    throw new ArgumentNullException("cipherText");
                if (string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("sharedSecret");

                // Declare the RijndaelManaged object
                // used to decrypt the data.
                RijndaelManaged aesAlg = null;

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                try
                {
                    // generate the key from the shared secret and the salt
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                    // Create the streams used for decryption.                
                    byte[] bytes = Convert.FromBase64String(cipherText);
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        // Create a RijndaelManaged object
                        // with the specified key and IV.
                        aesAlg = new RijndaelManaged();
                        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                        // Get the initialization vector from the encrypted stream
                        aesAlg.IV = ReadByteArray(msDecrypt);
                        // Create a decrytor to perform the stream transform.
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return plaintext;
            }
            catch (Exception ex)
            {
                return cipherText;
            }

        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
