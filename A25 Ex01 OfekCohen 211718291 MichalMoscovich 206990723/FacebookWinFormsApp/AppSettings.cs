using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class AppSettings
    {
        private const string k_AppSettingsFileName = "appConfig.xml";
        private static readonly string sr_SaveSettingsFilePath = string.Format($"{AppDomain.CurrentDomain.BaseDirectory}//{k_AppSettingsFileName}");

        public bool RememberLoggedInUser { get; set; }
        public string AccessToken { get; set; }
        public Point LastWindowLocation { get; set; }
        public Size LastWindowSize { get; set; }

        private AppSettings()
        {            
            if (!File.Exists(sr_SaveSettingsFilePath))
            {
                File.Create(sr_SaveSettingsFilePath).Dispose();
            }
        }

        public void SaveToFile()
        {
            using (Stream stream = new FileStream(sr_SaveSettingsFilePath, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                serializer.Serialize(stream, this);
            }
        }

        public static AppSettings LoadFromFile()
        {
            AppSettings appSettingsFromFile = null;
            Stream stream = null;

            try
            {
                stream = new FileStream(sr_SaveSettingsFilePath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                appSettingsFromFile = serializer.Deserialize(stream) as AppSettings;
            }
            catch (Exception)
            {
                appSettingsFromFile = new AppSettings();
            }
            finally
            {
                stream?.Dispose();
            }

            return appSettingsFromFile;
        }
    }
}
