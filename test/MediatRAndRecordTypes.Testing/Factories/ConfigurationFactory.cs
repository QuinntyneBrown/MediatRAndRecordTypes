using Microsoft.Extensions.Configuration;
using System.IO;

namespace MediatRAndRecordTypes.Testing.Factories
{
    public static class ConfigurationFactory
    {
        private static IConfiguration _configuration;

        public static IConfiguration Create()
        {
            if (_configuration == null)
            {
                var basePath = Path.GetFullPath(@$"../../../../../src/MediatRAndRecordTypes.Api");

                _configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json")
                    .Build();
            }

            return _configuration;
        }
    }
}
