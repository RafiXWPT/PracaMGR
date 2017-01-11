﻿using System;
using System.Configuration;

namespace InstitutionService.Host.Code
{
    public sealed class ConfigurationProvider
    {
        private static readonly Lazy<ConfigurationProvider> Lazy = new Lazy<ConfigurationProvider>(() => new ConfigurationProvider());

        public static ConfigurationProvider Instance => Lazy.Value;

        private KeyValueConfigurationCollection InstitutionConfiguration { get; }

        private ConfigurationProvider()
        {
            InstitutionConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;
        }

        public string GetValue(string key)
        {
            return HasValue(key) ? InstitutionConfiguration[key].Value : string.Empty;
        }

        public bool SetValue(string key, string value)
        {
            try
            {
                InstitutionConfiguration[key].Value = value;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private bool HasValue(string key)
        {
            try
            {
                return !string.IsNullOrEmpty(InstitutionConfiguration[key].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string GetInstitutionName()
        {
            return GetValue("INSTITUTION_NAME");
        }
    }
}
