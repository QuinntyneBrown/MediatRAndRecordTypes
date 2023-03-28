// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.ConsultAggregateModel;
using System;


namespace MediatRAndRecordTypes.Testing.Builders;

public class ConsultBuilder
{
    private Consult _consult;

    public static Consult WithDefaults()
    {
        return new Consult(default, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));
    }

    public ConsultBuilder()
    {
        _consult = new Consult(default, default, default);
    }

    public Consult Build()
    {
        return _consult;
    }
}

