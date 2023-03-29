// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;


namespace MediatRAndRecordTypes.Testing;

public class MediatRAndRecordTypesApiFactory : WebApplicationFactory<Program>
{
    private readonly IConfiguration _configuration;

    public MediatRAndRecordTypesApiFactory()
    {
        _configuration = ConfigurationFactory.Create();
    }
}

