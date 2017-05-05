using System.Configuration;

namespace HomeLibraryWeb.Configuration
{
    public sealed class ApiKeysConfiguration : ConfigurationSection
    {
        private static ApiKeysConfiguration _instance;

        [ConfigurationProperty("googleApiKey", IsRequired = true)]
        public string GoogleApiKey
        {
            get { return (string) this["googleApiKey"]; }
            set { this["googleApiKey"] = value; }
        }

        [ConfigurationProperty("applicationName", IsRequired = true)]
        public string ApplicationName
        {
            get { return (string)this["applicationName"]; }
            set { this["applicationName"] = value; }
        }

        public static ApiKeysConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (ApiKeysConfiguration)ConfigurationManager.GetSection("apiKeys");
                }

                return _instance;
            }
        }
    }
}