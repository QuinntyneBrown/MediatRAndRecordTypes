using MediatRAndRecordTypes.Api.Features;
using MediatRAndRecordTypes.Api.Features.Consults;
using MediatRAndRecordTypes.Api.Models;
using MediatRAndRecordTypes.Testing;
using MediatRAndRecordTypes.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MediatRAndRecordTypes.IntegrationTests
{
    public class ConsultsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ConsultsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_CreateConsult()
        {
            var context = AppDbContextBuilder.WithDefaults();

            var consult = ConsultDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { consult }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateConsult, stringContent);

            var response = JsonConvert.DeserializeObject<CreateConsult.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Consult>(response.Consult.ConsultId);

            Assert.NotEqual(default, response.Consult.ConsultId);
        }

        [Fact]
        public async Task Should_RemoveConsult()
        {
            var consult = ConsultBuilder.WithDefaults();

            var context = AppDbContextBuilder.WithDefaults();

            context.Add(consult);

            context.SaveChanges();

            var httpResponseMessage = await _fixture.CreateClient().DeleteAsync(Endpoints.Delete.ConsultBy(consult.ConsultId));

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_UpdateConsult()
        {
            var consult = ConsultBuilder.WithDefaults();

            var context = AppDbContextBuilder.WithDefaults();

            context.Add(consult);

            context.SaveChanges();

            var newEndDate = DateTime.UtcNow;

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { consult = consult.ToDto() with { EndDate = newEndDate } }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateClient().PutAsync(Endpoints.Put.UpdateConsult, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();
            
            context.ChangeTracker.Clear();

            var sut = await context.FindAsync<Consult>(consult.ConsultId);

            Assert.Equal(newEndDate, sut.DateRange.EndDate);
        }

        [Fact]
        public async Task Should_GetConsults()
        {
            var consult = ConsultBuilder.WithDefaults();

            var context = AppDbContextBuilder.WithDefaults();

            context.Add(consult);

            context.SaveChanges();

            var httpResponseMessage = await _fixture.CreateClient().GetAsync(Endpoints.Get.Consults);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConsults.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Consults.Any());
        }

        [Fact]
        public async Task Should_GetConsultById()
        {
            var consult = ConsultBuilder.WithDefaults();

            var context = AppDbContextBuilder.WithDefaults();

            context.Add(consult);

            context.SaveChanges();

            var httpResponseMessage = await _fixture.CreateClient().GetAsync(Endpoints.Get.ConsultBy(consult.ConsultId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConsultById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.Equal(consult.DateRange.StartDate, response.Consult.StartDate);

            Assert.Equal(consult.DateRange.EndDate, response.Consult.EndDate);
        }


        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateConsult = "api/consults";
            }

            public static class Put
            {
                public static string UpdateConsult = "api/consults";
            }

            public static class Delete
            {
                public static string ConsultBy(Guid consultId)
                {
                    return $"api/consults/{consultId}";
                }
            }

            public static class Get
            {
                public static string Consults = "api/consults";
                public static string ConsultBy(Guid consultId)
                {
                    return $"api/consults/{consultId}";
                }
            }
        }
    }
}
