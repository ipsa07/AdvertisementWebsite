using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AdvertiseESWriter
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration = null;
        public static IConfiguration Instance
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                }

                return _configuration;
            }
        }
    }
}
