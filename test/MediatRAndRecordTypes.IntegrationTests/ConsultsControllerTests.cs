using MediatRAndRecordTypes.Api.Features.Consults;
using MediatRAndRecordTypes.Api.Models;
using MediatRAndRecordTypes.Testing;
using MediatRAndRecordTypes.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MediatRAndRecordTypes.IntegrationTests
{
    [Collection("Consults")]
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

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.AddConsult, stringContent);

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

        internal static class Endpoints
        {
            public static class Post
            {
                public static string AddConsult = "api/consults";
            }

            public static class Delete
            {
                public static string ConsultBy(Guid consultId)
                {
                    return $"api/consults/{consultId}";
                }
            }
        }
    }
}
