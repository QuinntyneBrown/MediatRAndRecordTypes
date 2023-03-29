// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.ConsultAggregateModel;
using MediatRAndRecordTypes.Api.ConsultAggregateModel.Commands;
using MediatRAndRecordTypes.Api.ConsultAggregateModel.Queries;
using MediatRAndRecordTypes.Testing;
using MediatRAndRecordTypes.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static MediatRAndRecordTypes.IntegrationTests.ConsultsControllerEndpoints;

namespace MediatRAndRecordTypes.IntegrationTests;

public class ConsultsControllerTests : IClassFixture<ApiTestFixture>
{
    private readonly ApiTestFixture _fixture;
    public ConsultsControllerTests(ApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_CreateConsult()
    {

        var context = MediatRAndRecordTypesDbContextBuilder.WithDefaults();

        var request = new CreateConsultRequest(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        using var client = _fixture.CreateClient();

        var httpResponseMessage = await client.PostAsync(Post.CreateConsult, stringContent);

        var response = JsonConvert.DeserializeObject<CreateConsultResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

        var sut = context.FindAsync<Consult>(response.Consult.ConsultId);

        Assert.NotEqual(default, response.Consult.ConsultId);

    }

    [Fact]
    public async Task Should_RemoveConsult()
    {
        var consult = ConsultBuilder.WithDefaults();

        var context = MediatRAndRecordTypesDbContextBuilder.WithDefaults();

        context.Add(consult);

        context.SaveChanges();

        var httpResponseMessage = await _fixture.CreateClient().DeleteAsync(Delete.ConsultBy(consult.ConsultId));

        httpResponseMessage.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Should_RescheduleConsult()
    {
        var consult = ConsultBuilder.WithDefaults();

        var context = MediatRAndRecordTypesDbContextBuilder.WithDefaults();

        context.Add(consult);

        context.SaveChanges();

        var newEndDate = DateTime.UtcNow;

        var request = new RescheduleRequest(consult.ConsultId, consult.DateRange.StartDate, newEndDate);

        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var httpResponseMessage = await _fixture.CreateClient().PutAsync(Put.Reschedule, stringContent);

        httpResponseMessage.EnsureSuccessStatusCode();

        context.ChangeTracker.Clear();

        var sut = await context.FindAsync<Consult>(consult.ConsultId);

        Assert.Equal(newEndDate, sut.DateRange.EndDate);
    }

    [Fact]
    public async Task Should_GetConsults()
    {
        var consult = ConsultBuilder.WithDefaults();

        var context = MediatRAndRecordTypesDbContextBuilder.WithDefaults();

        context.Add(consult);

        context.SaveChanges();

        var httpResponseMessage = await _fixture.CreateClient().GetAsync(Get.Consults);

        httpResponseMessage.EnsureSuccessStatusCode();

        var response = JsonConvert.DeserializeObject<GetConsultsResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

        Assert.True(response.Consults.Any());
    }

    [Fact]
    public async Task Should_GetConsultById()
    {
        var consult = ConsultBuilder.WithDefaults();

        var context = MediatRAndRecordTypesDbContextBuilder.WithDefaults();

        context.Add(consult);

        context.SaveChanges();

        var httpResponseMessage = await _fixture.CreateClient().GetAsync(Get.ConsultBy(consult.ConsultId));

        httpResponseMessage.EnsureSuccessStatusCode();

        var response = JsonConvert.DeserializeObject<GetConsultByIdResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

        Assert.Equal(consult.DateRange.StartDate, response.Consult.StartDate);

        Assert.Equal(consult.DateRange.EndDate, response.Consult.EndDate);
    }
}

