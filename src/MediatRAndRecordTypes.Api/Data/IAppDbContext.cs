using MediatRAndRecordTypes.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Data
{
    public interface IAppDbContext
    {
        DbSet<Consult> Consults { get; }
        EntityEntry Add(object entity);
        EntityEntry Remove(object entity);
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ChangeTracker ChangeTracker { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IAppDbContext AsNoTracking();
    }
}
