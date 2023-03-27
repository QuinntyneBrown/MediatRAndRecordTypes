// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace MediatRAndRecordTypes.Api.Data;

public class MediatRAndRecordTypesDbContext : DbContext, IMediatRAndRecordTypesDbContext
{
    public DbSet<Consult> Consults { get; private set; }

    public MediatRAndRecordTypesDbContext(DbContextOptions options)
        : base(options) { }

    public IMediatRAndRecordTypesDbContext AsNoTracking()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        return this;
    }
}

