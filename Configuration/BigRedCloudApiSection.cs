using System.Configuration;

namespace BigRedCloud.Api.Configuration
{
    public class BigRedCloudApiSection : ConfigurationSection
    {
        [ConfigurationProperty("apiServerUrl", IsRequired = true)]
        public string ApiServerUrl
        {
            get { return (string)this["apiServerUrl"]; }
            set { this["apiServerUrl"] = value; }
        }

        [ConfigurationProperty("apiKeys", IsRequired = false)]
        public ApiKeyElementCollection ApiKeys
        {
            get { return ((ApiKeyElementCollection)(this["apiKeys"])); }
            set { this["apiKeys"] = value; }
        }
    }
}
