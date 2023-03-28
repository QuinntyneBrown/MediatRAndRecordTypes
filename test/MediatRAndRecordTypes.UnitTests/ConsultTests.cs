// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using CSharpFunctionalExtensions;
using MediatRAndRecordTypes.Api.ConsultAggregateModel;
using MediatRAndRecordTypes.Testing.Builders;
using System;
using Xunit;
using static System.DateTime;
using static System.Guid;


namespace MediatRAndRecordTypes.UnitTests;

public class ConsultTests
{
    [Fact]
    public void Should_CreateValidConsult()
    {
        var context = new MediatRAndRecordTypesDbContextBuilder()
            .UseInMemoryDatabase()
            .Build();

        var consult = new Consult(NewGuid(), startDate: UtcNow, endDate: UtcNow);

        consult.EnsureAvailability(context);
    }

    [Fact]
    public void Should_ThrowExceptionWhenCreateConsultWithEndDateBeforeStartDate()
    {
        Assert.Throws<ResultFailureException>(() => new Consult(NewGuid(), startDate: UtcNow, endDate: UtcNow.AddHours(-1)));
    }

    [Fact]
    public void Should_ThrowExceptionWhenBookingOverlapingConsults()
    {
        var context = new MediatRAndRecordTypesDbContextBuilder()
            .UseInMemoryDatabase()
            .Add(new Consult(NewGuid(), startDate: UtcNow, endDate: UtcNow.AddHours(1)))
            .SaveChanges()
            .Build();

        var consult = new Consult(NewGuid(), startDate: UtcNow.AddHours(-1), endDate: UtcNow.AddHours(.5));

        Assert.Throws<Exception>(() => consult.EnsureAvailability(context));
    }
}

