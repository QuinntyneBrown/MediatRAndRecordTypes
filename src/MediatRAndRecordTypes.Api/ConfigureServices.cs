// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "",
                Description = "",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "",
                    Email = ""
                },
                License = new OpenApiLicense
                {
                    Name = "Use under MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT"),
                }
            });

            options.CustomSchemaIds(x => x.FullName);
        });

        services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowCredentials()));

        services.AddHttpContextAccessor();

        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(GetConsults)));

        services.AddScoped<IMediatRAndRecordTypesDbContext, MediatRAndRecordTypesDbContext>();

        services.AddDbContextPool<MediatRAndRecordTypesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly("MediatRAndRecordTypes.Api")
                    .EnableRetryOnFailure())
            .EnableThreadSafetyChecks(false)
            .EnableSensitiveDataLogging();
        });

        services.AddControllers();
    }

}


