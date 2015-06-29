using System;
using System.Configuration;

namespace TaskManager.Infrastructure.Config
{
    public class ConfigHelper
    {
        private const string configGroupName = "taskManagerConfiguration/";

        internal static type Get<type>() where type : ISettings, new ()
        {
            string configSectionName = (new type()).ConfigSectionName;
            var config = (type)ConfigurationManager
                 .GetSection(String.Concat(configGroupName, configSectionName));
            return config;
        }
    }
}