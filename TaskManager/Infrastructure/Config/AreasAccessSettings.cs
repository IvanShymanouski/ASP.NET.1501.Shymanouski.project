using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TaskManager.Infrastructure.Config
{
    public class AreasAccessSettings : ConfigurationSection, ISettings
    {
        #region fields
        public string ConfigSectionName 
        {
            get { return configSectionName; }
            private set { }
        }

        private const string sectionName = "areasAccess";
        private const string configSectionName = sectionName + "Configuration";
        #endregion

        [ConfigurationProperty(sectionName, IsRequired = true)]
        public AreasAccessCollection AreasAccess
        {
            get { return (AreasAccessCollection)base[sectionName]; }
        }
    }

    #region AreaAccess
    [ConfigurationCollection(typeof(AreaAccessElement), AddItemName = "add")]
    public class AreasAccessCollection : ConfigurationElementCollection
    {
        public AreaAccessElement this[int index]
        {
            get { return (AreaAccessElement)BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AreaAccessElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return new { ((AreaAccessElement)element).Name, ((AreaAccessElement)element).Roles, ((AreaAccessElement)element).Users };
        }
    }

    public class AreaAccessElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "Guest", IsRequired = true)]
        [StringValidator(MinLength = 3, MaxLength = 50,
               InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("roles", DefaultValue = "", IsRequired = true)]
        public String Roles
        {
            get { return (String)this["roles"]; }
            set { this["roles"] = value; }
        }

        [ConfigurationProperty("users", DefaultValue = "", IsRequired = false)]
        public String Users
        {
            get { return (String)this["users"]; }
            set { this["users"] = value; }
        }
    }
    #endregion        
}