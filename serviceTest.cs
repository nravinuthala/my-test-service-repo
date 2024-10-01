using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyTestService.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
        }
    }

    public class MyControllerTests : IClassFixture<CustomWebApplicationFactory<MyTestService.Startup>>
    {
        private readonly HttpClient _client;

        public MyControllerTests(CustomWebApplicationFactory<MyTestService.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetHello_ReturnsHelloMessage()
        {
            // Arrange
            var request = "/api/hello";

            // Act
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello from the GET method!", responseString);
        }

        [Fact]
        public async Task GetRoot_ReturnsHelloWorld()
        {
            // Arrange
            var request = "/";

            // Act
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello World!", responseString);
        }
    }
}




























































