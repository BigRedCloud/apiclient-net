using System;
using System.Configuration;

namespace BigRedCloud.Api.Configuration
{
    [ConfigurationCollection(typeof(ApiKeyElement))]
    public class ApiKeyElementCollection : ConfigurationElementCollection
    {
        private const string PropertyName = "apiKey";

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return PropertyName; }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiKeyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiKeyElement)(element)).Name;
        }

        public ApiKeyElement this[int idx]
        {
            get { return (ApiKeyElement)BaseGet(idx); }
        }
    }
}
