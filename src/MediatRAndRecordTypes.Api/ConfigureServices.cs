// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.ConsultAggregateModel.Queries;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                Title = "MediatR And Record Types Api",
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

        });

        services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(isOriginAllowed: _ => true)
            .AllowCredentials()));

        services.AddHttpContextAccessor();

        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(GetConsultsRequest)));

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


