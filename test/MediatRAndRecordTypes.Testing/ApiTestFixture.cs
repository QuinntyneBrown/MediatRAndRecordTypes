// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Testing.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;


namespace MediatRAndRecordTypes.Testing;

public class ApiTestFixture : WebApplicationFactory<Program>
{
    private readonly IConfiguration _configuration;

    public ApiTestFixture()
    {
        _configuration = ConfigurationFactory.Create();
    }
}

