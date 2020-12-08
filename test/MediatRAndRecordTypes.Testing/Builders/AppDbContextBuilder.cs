using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.EntityFrameworkCore;
using System;

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

        public AppDbContextBuilder UseInMemoryDatabase()
        {
            _testDbContext = new AppDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);

            return this;
        }

        public AppDbContextBuilder Add(object entity)
        {
            _testDbContext.Add(entity);

            return this;
        }

        public AppDbContextBuilder SaveChanges()
        {
            _testDbContext.SaveChanges();

            return this;
        }

        public AppDbContext Build()
        {
            return _testDbContext;
        }
    }
}
