// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger(options => options.SerializeAsV2 = true);

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clarity");
    options.RoutePrefix = string.Empty;
    options.DisplayOperationId();
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

var services = (IServiceScopeFactory)app.Services.GetRequiredService(typeof(IServiceScopeFactory));

using (var scope = services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MediatRAndRecordTypesDbContext>();

    if (args.Contains("ci"))
        args = new string[] { "dropdb", "migratedb", "stop" };

    if (args.Contains("dropdb"))
    {
        context.Database.ExecuteSql($"DROP TABLE Consults");

        context.Database.ExecuteSql($"DROP SCHEMA MediatRAndRecordTypes");

        context.Database.ExecuteSql($"DELETE from __EFMigrationsHistory where MigrationId like '%_MediatRAndRecordTypes_%';");
    }

    if (args.Contains("migratedb"))
    {
        context.Database.Migrate();
    }

    if (args.Contains("stop"))
        Environment.Exit(0);
}

app.Run();



public partial class Program { }