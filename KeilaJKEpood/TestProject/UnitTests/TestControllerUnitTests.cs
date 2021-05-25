using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Controllers;
using WebApp.ViewModels.Test;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace TestProject.UnitTests
{
    public class TestControllerUnitTests
    {
        private readonly TestController _testController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        
        // ARRANGE - common
        public TestControllerUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();
            
            // SUT
            _testController = new TestController(logger,_ctx);
        }


        [Fact]
        public async Task Action_Test__Returns_ViewModel()
        {
            // ACT
            var result = await _testController.Test();
            
            // ASSERT
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            var vm = viewResult!.Model;
            Assert.IsType<TestViewModel>(vm);
            var testVm = vm as TestViewModel;
            Assert.NotNull(testVm!.PaymentTypes);
            // for debugging
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            Assert.Equal(0, testVm.PaymentTypes.Count);
        }

        [Fact]
        public async Task Action_Test__Returns_ViewModel_WithData()
        {
            // ARRANGE
            await SeedData();
            
            // ACT
            var result = await _testController.Test();
            
            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            Assert.NotNull(testVm);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            Assert.Equal(1, testVm!.PaymentTypes.Count);
            Assert.Equal("Type 0", testVm.PaymentTypes.First()!.PaymentTypeName!.ToString()); 
        }
        
        [Fact]
        public async Task Action_Test__Returns_ViewModel_WithNoData__Fails_With_Exception()
        {
            
            // ACT
            var result = await _testController.Test();
            
            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            Assert.NotNull(testVm);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.PaymentTypes.Count}");

            Assert.ThrowsAny<Exception>(() => testVm!.PaymentTypes.First());
        }
        
        
        [Theory]
        //[InlineData(1)]
        //[InlineData(5)]
        [ClassData(typeof(CountGenerator))]
        public async Task Action_Test__Returns_ViewModel_WithData_Fluent(int count)
        {
            // ARRANGE
            await SeedData(count);
            
            // ACT
            var result = await _testController.Test();
            
            // ASSERT
            var testVm = (result as ViewResult)?.Model as TestViewModel;
            testVm.Should().NotBeNull();
            testVm!.PaymentTypes
                .Should().NotBeNull()
                .And.HaveCount(count)
                .And.Contain(ct => ct.PaymentTypeName!.ToString() == "Type 0")
                .And.Contain(ct => ct.PaymentTypeName!.ToString() == $"Type {count-1}");
        }
        
        
        
        private async Task SeedData(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                _ctx.PaymentTypes.Add(new PaymentType()
                {
                    PaymentTypeName = new LangString($"Type {i}")
                });
            }
            await _ctx.SaveChangesAsync();
        }
    }


    public class CountGenerator : IEnumerable<object[]>
    {
        private static List<object[]> _data 
        {
            get
            {
                var res = new List<Object[]>();
                for (int i = 1; i <= 100; i++)
                {
                    res.Add(new object[]{i});
                }

                return res;
            }
        }
        
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
