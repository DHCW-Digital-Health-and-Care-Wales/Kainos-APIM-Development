using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Search_NHSNumber_RecordFound()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
            HttpResponseMessage result = await client.GetAsync("/Patient/8888842799");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Search_NHSNumber_RecordNotFound()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
            HttpResponseMessage result = await client.GetAsync("/Patient/7777742799");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Search_NHSNumber_Timeout()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
            HttpResponseMessage result = await client.GetAsync("/Patient/1111142799");

            // Assert
            Assert.Equal(HttpStatusCode.RequestTimeout, result.StatusCode);
        }

        [Fact]
        public async Task Search_NHSNumber_InvalidNHSNumber()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
            HttpResponseMessage result = await client.GetAsync("/Patient/28");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Search_NHSNumber_InternalServerError()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
            HttpResponseMessage result = await client.GetAsync("/Patient");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Search_NHSNumber_Unauthorised()
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();

            // Perform action
            HttpResponseMessage result = await client.GetAsync("/Patient/8888842799");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

    }
}
