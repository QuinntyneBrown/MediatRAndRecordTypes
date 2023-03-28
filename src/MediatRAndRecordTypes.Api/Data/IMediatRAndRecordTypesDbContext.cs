// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.ConsultAggregateModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MediatRAndRecordTypes.Api.Data;

public interface IMediatRAndRecordTypesDbContext
{
    DbSet<Consult> Consults { get; }
    EntityEntry Add(object entity);
    EntityEntry Remove(object entity);
    ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    IMediatRAndRecordTypesDbContext AsNoTracking();
}

