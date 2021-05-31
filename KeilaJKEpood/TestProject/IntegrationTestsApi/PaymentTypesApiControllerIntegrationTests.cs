using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;


namespace TestProject.IntegrationTestsApi
{
    public class PaymentTypesApiControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public PaymentTypesApiControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }
        
        [Fact]
        public async Task Api_Get_ContactTypes()
        {
            // ARRANGE
            var uri = "/api/v1/PaymentTypes";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            
            

            var data = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.PaymentType>>(body);

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Single(data);
            Assert.Equal("Type 0", data![0].PaymentTypeName);
        }


    }
}