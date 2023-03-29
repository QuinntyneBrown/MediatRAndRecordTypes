// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace MediatRAndRecordTypes.Testing.Builders;

public class MediatRAndRecordTypesDbContextBuilder
{
    private MediatRAndRecordTypesDbContext _testDbContext;

    public static MediatRAndRecordTypesDbContext WithDefaults()
    {
        var configuration = ConfigurationFactory.Create();

        return new MediatRAndRecordTypesDbContext(new DbContextOptionsBuilder()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .Options);
    }

    public MediatRAndRecordTypesDbContextBuilder()
    {
        _testDbContext = WithDefaults();
    }

    public MediatRAndRecordTypesDbContextBuilder UseInMemoryDatabase()
    {
        _testDbContext = new MediatRAndRecordTypesDbContext(new DbContextOptionsBuilder()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

        return this;
    }

    public MediatRAndRecordTypesDbContextBuilder Add(object entity)
    {
        _testDbContext.Add(entity);

        return this;
    }

    public MediatRAndRecordTypesDbContextBuilder SaveChanges()
    {
        _testDbContext.SaveChanges();

        return this;
    }

    public MediatRAndRecordTypesDbContext Build()
    {
        return _testDbContext;
    }
}

