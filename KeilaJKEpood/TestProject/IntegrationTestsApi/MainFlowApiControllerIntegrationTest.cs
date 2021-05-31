using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using DTO.App;
using Xunit.Abstractions;

namespace TestProject.IntegrationTestsApi
{
    public class MainFlowApiControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public MainFlowApiControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory,
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
        public async Task Api_MainFlow()
        {
            // ARRANGE
            var uri = "/api/v1/Account/register";

            // ACT
            var regFormValues = new DTO.App.Register()
            {
                Email = "foo@gmail.com",
                Firstname = "foo",
                Lastname = "Bar",
                Password = "Password1.",
                Userlevel = "2"
            };
            var jsonRegFormValues = JsonSerializer.Serialize(regFormValues);
            HttpContent content = new StringContent(jsonRegFormValues, Encoding.UTF8, "application/json");
            
            var getRegistrationResponse = await _client.PostAsync(uri, content);
            getRegistrationResponse.EnsureSuccessStatusCode();
            
            var body = await getRegistrationResponse.Content.ReadAsStringAsync();
            
            var data = JsonHelper.DeserializeWithWebDefaults<DTO.App.JwtResponse>(body);
            
            _client.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", data!.Token);
 
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(data!.Token);
            var tokenS = jsonToken as JwtSecurityToken;

            var userId = tokenS!.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            

            var productsUri = "/api/v1/Products";
            
            var getProductsResponse = await _client.GetAsync(productsUri);
            
            getProductsResponse.EnsureSuccessStatusCode();

            var productBody = await getProductsResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(productBody);

            var productData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Product>>(productBody);

            var productId = productData![0].Id;
            
            // Create order
            var ordersUri = "/api/v1/Orders";

            var orderId = Guid.NewGuid();
            
            var order = new DTO.App.OrderAdd()
            {
                id = orderId.ToString(),
                userId = userId
            };
            
            var jsonOrder = JsonSerializer.Serialize(order);
            HttpContent orderContent = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            
            var getOrderResponse = await _client.PostAsync(ordersUri, orderContent);
            getOrderResponse.EnsureSuccessStatusCode();
            
            
            // Create product in order
            var productInOrdersUri = "/api/v1/ProductsInOrders";
            var prodInOrderId = Guid.NewGuid();
            var productInOrder = new BLL.App.DTO.ProductInOrder()
            {
                Id = prodInOrderId,
                ProductId = productId,
                OrderId = orderId,
                ProductAmount = 1,
                From = DateTime.Now
            };
            
            var jsonProductInOrder = JsonSerializer.Serialize(productInOrder);
            HttpContent productInOrderContent = new StringContent(jsonProductInOrder, Encoding.UTF8, "application/json");
            
            var getProductInOrderResponse = await _client.PostAsync(productInOrdersUri, productInOrderContent);
            getOrderResponse.EnsureSuccessStatusCode();
            
            // Create person
            var personsUri = "/api/v1/Persons";
            var person = new DTO.App.PersonAdd()
            {
                FirstName = "Ats",
                LastName = "Purje",
                PersonsIdCode = "123123"
            };
            
            var jsonPerson = JsonSerializer.Serialize(person);
            HttpContent personContent = new StringContent(jsonPerson, Encoding.UTF8, "application/json");
            
            var getPersonResponse = await _client.PostAsync(personsUri, personContent);
            getOrderResponse.EnsureSuccessStatusCode();
            
            
            var getPersonGetResponse = await _client.GetAsync(personsUri);
            
            getPersonGetResponse.EnsureSuccessStatusCode();

            var personBody = await getPersonGetResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(personBody);

            var personData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Person>>(personBody);

            var personId = personData![0].Id;
            
            
            // Create bill
            var billsUri = "/api/v1/Bills";

            var bill = new DTO.App.BillAdd()
            {
                PersonId = personId.ToString(),
                UserId = userId,
                OrderId = orderId.ToString(),
                PriceToPay = 5,
                PriceWithoutTax = 0,
                SumOfTax = 0
            };
            
            var jsonBill = JsonSerializer.Serialize(bill);
            HttpContent billContent = new StringContent(jsonBill, Encoding.UTF8, "application/json");
            
            var getBillResponse = await _client.PostAsync(billsUri, billContent);
            getBillResponse.EnsureSuccessStatusCode();
            
            var getBillGetResponse = await _client.GetAsync(billsUri);
            
            getBillGetResponse.EnsureSuccessStatusCode();

            var billBody = await getBillGetResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(billBody);

            var billData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Bill>>(billBody);

            var billId = billData![0].Id;
            
            // Get price ID
            var pricesUri = "/api/v1/Prices";
            var getPriceResponse = await _client.GetAsync(pricesUri);
            
            getPriceResponse.EnsureSuccessStatusCode();

            var priceBody = await getPriceResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(priceBody);

            var priceData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Price>>(priceBody);

            var priceId = priceData![0].Id;
            // Create lineOnBill
            var lineOnBillsUri = "/api/v1/LinesOnBills";

            var lineOnBill = new DTO.App.LineOnBillAdd()
            {
                BillId = billId.ToString(),
                PriceId= priceId.ToString(),
                ProductId = productId.ToString(),
                Amount = 0,
                TaxPercentage = 0,
                PriceToPay = 5,
                PriceWithoutTax = 0,
                SumOfTax = 0
            };
            
            var jsonLineOnBill = JsonSerializer.Serialize(lineOnBill);
            HttpContent lineOnBillContent = new StringContent(jsonLineOnBill, Encoding.UTF8, "application/json");
            
            var getLineOnBillResponse = await _client.PostAsync(lineOnBillsUri, lineOnBillContent);
            getLineOnBillResponse.EnsureSuccessStatusCode();
            
            // Get payment type Id
            // ARRANGE
            var paymentTypeUri = "/api/v1/PaymentTypes";

            // ACT
            var getPaymentTypeResponse = await _client.GetAsync(paymentTypeUri);

            // ASSERT
            getPaymentTypeResponse.EnsureSuccessStatusCode();

            var paymentTypeBody = await getPaymentTypeResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(paymentTypeBody);
            
            

            var paymentTypeData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.PaymentType>>(paymentTypeBody);
            
            var paymentTypeId = paymentTypeData![0].Id;
            
            // Create Payment
            
            var paymentsUri = "/api/v1/Payments";
            var paymentId = Guid.NewGuid();
            var payment = new DTO.App.PaymentAdd()
            {
                Id = paymentId.ToString(),
                PaymentTypeId = paymentTypeId.ToString(),
                BillId = billId.ToString(),
                PersonId = personId.ToString()
            };
            
            var jsonPayment = JsonSerializer.Serialize(payment);
            HttpContent paymentContent = new StringContent(jsonPayment, Encoding.UTF8, "application/json");
            
            var getPaymentResponse = await _client.PostAsync(paymentsUri, paymentContent);
            getPaymentResponse.EnsureSuccessStatusCode();
            
            var getPaymentGetResponse = await _client.GetAsync(paymentsUri);
            
            getPaymentGetResponse.EnsureSuccessStatusCode();

            var paymentBody = await getPaymentGetResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(paymentBody);

            var paymentData = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Payment>>(paymentBody);
            
            
            // ASSERT
            
            Assert.NotNull(paymentData);
            Assert.NotEmpty(paymentData);
            Assert.Single(paymentData);
            Assert.Equal(billId.ToString(), paymentData![0].BillId.ToString());
        }
        
    }
}