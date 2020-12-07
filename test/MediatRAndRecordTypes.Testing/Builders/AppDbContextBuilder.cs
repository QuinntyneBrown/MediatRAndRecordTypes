using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndRecordTypes.Testing.Builders
{
    public class AppDbContextBuilder
    {
        private AppDbContext _testDbContext;

        public static AppDbContext WithDefaults()
        {
            var configuration = ConfigurationFactory.Create();

            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }

        public AppDbContextBuilder()
        {
            _testDbContext = WithDefaults();
        }

        public AppDbContext Build()
        {
            return _testDbContext;
        }
    }
}
