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
}
