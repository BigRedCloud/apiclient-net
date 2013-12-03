using System.Configuration;

namespace BigRedCloud.Api.Configuration
{
    public class ApiKeyElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }

        [ConfigurationProperty("isDefault", IsRequired = false, DefaultValue = "false")]
        public bool IsDefault
        {
            get { return (bool)this["isDefault"]; }
            set { this["isDefault"] = value; }
        }
    }
}
