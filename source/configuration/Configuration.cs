using System;
using Microsoft.Extensions.Configuration;

namespace Managed.Identity.Configuration
{
    public sealed class Configuration
    {
        private static volatile IConfiguration instance;
        private static readonly object lockObject = new Object();
        
        private Configuration() { }

        public static IConfiguration Instance
        {
            get {
                if (instance == null) {
                    lock(lockObject) {
                        if (instance == null) {
                            IConfigurationRoot config = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.local.json")
                                .Build();
                            instance = config;
                        }
                    }
                }

                return instance;
            }
        }
    }
}
