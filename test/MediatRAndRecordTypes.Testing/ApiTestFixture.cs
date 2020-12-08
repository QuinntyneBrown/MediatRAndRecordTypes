using MediatRAndRecordTypes.Api;
using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Respawn;

namespace MediatRAndRecordTypes.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>
    {
        private readonly IConfiguration _configuration;
        private readonly Checkpoint _checkpoint;

        public ApiTestFixture()
        {
            _configuration = ConfigurationFactory.Create();


            _checkpoint = new Checkpoint()
            {
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
            };

            _checkpoint.Reset(_configuration["Data:DefaultConnection:ConnectionString"]);
        }
    }
}
