// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRAndRecordTypes.Testing;

public class MediatRAndRecordTypesApiFactory : WebApplicationFactory<Program>
{
    private readonly IConfiguration _configuration;

    public MediatRAndRecordTypesApiFactory()
    {
        _configuration = ConfigurationFactory.Create();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();

            var scope = serviceProvider.CreateScope();

            var scopedServices = scope.ServiceProvider;

            var context = scopedServices.GetRequiredService<MediatRAndRecordTypesDbContext>();

            context.Database.ExecuteSql($"DROP TABLE [MediatRAndRecordTypes].[Consults]");

            context.Database.ExecuteSql($"DROP SCHEMA [MediatRAndRecordTypes]");

            context.Database.ExecuteSql($"DELETE from __EFMigrationsHistory where MigrationId like '%_MediatRAndRecordTypes_%';");

            context.Database.Migrate();
        });
    }

}

