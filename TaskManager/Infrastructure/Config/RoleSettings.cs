using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TaskManager.Infrastructure.Config
{
    public class RolesSettings : ConfigurationSection, ISettings
    {
        #region fields
        public string ConfigSectionName
        {
            get { return configSectionName; }
            private set { }
        }

        private const string sectionName = "roles";
        private const string configSectionName = sectionName + "Configuration";
        #endregion

        [ConfigurationProperty(sectionName, IsRequired = true)]
        public RolesCollection Roles
        {
            get { return (RolesCollection)base[sectionName]; }
        }
    }

    #region Role
    [ConfigurationCollection(typeof(RoleElement), AddItemName = "add")]
    public class RolesCollection : ConfigurationElementCollection
    {
        public RoleElement this[int index]
        {
            get { return (RoleElement)BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RoleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return new { ((RoleElement)element).Name, ((RoleElement)element).Key };
        }
    }

    public class RoleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "Guest", IsRequired = true)]
        [StringValidator(MinLength = 3, MaxLength = 50,
               InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("guid", DefaultValue = "00000000-0000-0000-0000-000000000000", IsRequired = true)]
        public Guid Key
        {
            get { return (Guid)this["guid"]; }
            set { this["guid"] = value.ToString(); }
        }
    }
    #endregion
}