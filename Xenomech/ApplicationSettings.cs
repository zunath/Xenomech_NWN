using System;

namespace Xenomech
{
    public class ApplicationSettings
    {
        public string LogDirectory { get; }
        public string RedisIPAddress { get; }

        private static ApplicationSettings _settings;
        public static ApplicationSettings Get()
        {
            if (_settings == null)
                _settings = new ApplicationSettings();

            return _settings;
        }

        private ApplicationSettings()
        {
            LogDirectory = Environment.GetEnvironmentVariable("XM_APP_LOG_DIRECTORY");
            RedisIPAddress = Environment.GetEnvironmentVariable("NWNX_REDIS_HOST");
        }

    }
}