﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class MainHappyFlowIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;


        public MainHappyFlowIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
        }

        [Fact]
        public async Task TestAction_HasSuccessStatusCode()
        {
            // ARRANGE
            var uri = "/test/test";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();
            Assert.InRange((int)getTestResponse.StatusCode, 200, 299);
        }
        
        [Fact]
        public async Task TestAuthAction_AuthRedirect()
        {
            // ARRANGE
            var uri = "/test/TestAuth";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
        }
        
        [Fact]
        public async Task TestAuthAction_AuthFlow()
        {
            // ARRANGE
            var uri = "/test/TestAuth";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
            var redirectUri = getTestResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();

            await Get_Login_Page(redirectUri);
            // we need to follow the redirect
            // get the login page
            // get the registration page
            // fill up the reg form
            // submit form
            // try to access auth resource again - we should have new user and be logged in
        }

        public async Task Get_Login_Page(string uri)
        {
            var getLoginPageResponse = await _client.GetAsync(uri);
            getLoginPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getLoginDocument = await HtmlHelpers.GetDocumentAsync(getLoginPageResponse);
            
            var registerAnchorElement = (IHtmlAnchorElement) getLoginDocument.QuerySelector("#register");
            var registerUrl = registerAnchorElement.Href;
            _testOutputHelper.WriteLine("Register url: " + registerUrl);

            await Get_Register_Page(registerUrl);
        }

        public async Task Get_Register_Page(string uri)
        {
            var getRegisterPageResponse = await _client.GetAsync(uri);
            getRegisterPageResponse.EnsureSuccessStatusCode();
            
            // get the document
            var getRegisterDocument = await HtmlHelpers.GetDocumentAsync(getRegisterPageResponse);

            var regForm = (IHtmlFormElement) getRegisterDocument.QuerySelector("#register-form");
            var regFormValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "test@user.ee",
                ["Input_Password"] = "Foo.bar1",
                ["Input_ConfirmPassword"] = "Foo.bar1",
                ["Input_FirstName"] = "Test",
                ["Input_LastName"] = "User",
            };

            var regPostResponse = await _client.SendAsync(regForm, regFormValues);

            regPostResponse.StatusCode.Should().Equals(302);
            
            var redirectUri = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            
            await Get_TestAuthAction_Authenticated(redirectUri);

        }

        public async Task Get_TestAuthAction_Authenticated(string uri)
        {
            var getTestResponse = await _client.GetAsync(uri);
            getTestResponse.EnsureSuccessStatusCode();
            
            _testOutputHelper.WriteLine($"Uri '{uri}' was accessed with response status code '{getTestResponse.StatusCode}'.");
        }
    }
}
