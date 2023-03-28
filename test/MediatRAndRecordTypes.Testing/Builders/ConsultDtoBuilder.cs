// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.ConsultAggregateModel;
using System;


namespace MediatRAndRecordTypes.Testing.Builders;

public class ConsultDtoBuilder
{
    private ConsultDto _consultDto;

    public static ConsultDto WithDefaults()
    {
        return new ConsultDto(default, default, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
    }

    public ConsultDtoBuilder()
    {
        _consultDto = WithDefaults();
    }

    public ConsultDto Build()
    {
        return _consultDto;
    }
}

