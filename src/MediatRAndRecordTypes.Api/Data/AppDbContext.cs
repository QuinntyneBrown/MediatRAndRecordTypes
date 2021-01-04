using MediatRAndRecordTypes.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatRAndRecordTypes.Api.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Consult> Consults { get; private set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => builder.AddConsole());
        
        public AppDbContext(DbContextOptions options)
            : base(options) { }
    }
}
