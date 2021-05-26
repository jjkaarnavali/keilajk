using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;
using DAL.App.EF;
using DAL.App.EF.Repositories;
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
using Moq;

namespace TestProject.UnitTests
{
    public class BaseEntityServiceUnitTests
    {
        private readonly TestController _testController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        
        
        private readonly BaseEntityService<IAppUnitOfWork, IPersonRepository, BLL.App.DTO.Person, DAL.App.DTO.Person> _service;
        private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public BaseEntityServiceUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            /*var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<BLL.App.DTO.Person, DAL.App.DTO.Person>());*/

            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new BLL.App.DTO.MappingProfiles.AutoMapperProfile());
                configuration.AddProfile(new DAL.App.DTO.MappingProfiles.AutoMapperProfile());
            });
            
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            

            _mapper = config.CreateMapper();
            
            
            _uow = new AppUnitOfWork(_ctx, _mapper);

            _bll = new AppBLL(_uow, _mapper);
            var personRepository = new PersonRepository(_ctx, _mapper);
            var personMapper = new BLL.App.Mappers.PersonMapper(_mapper);
            
            _service = new BaseEntityService<IAppUnitOfWork, IPersonRepository, BLL.App.DTO.Person, DAL.App.DTO.Person>(_uow, personRepository, personMapper);
            
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();
            
            // SUT
            _testController = new TestController(logger,_ctx);
            
            /*// set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();*/

            /*using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();
            
            // SUT
            _testController = new TestController(logger,_ctx);*/
        }
        
        [Fact]
        public async Task Action_Test_Add()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            _service.Add(person);
            await _bll.SaveChangesAsync();
            
            var result = await _bll.Persons.GetAllAsync();
            
            // ASSERT
            var found = _service.ExistsAsync(id);
            var fakeId = Guid.NewGuid();
            var fakeNotFound = _service.ExistsAsync(fakeId);

            Assert.NotNull(result);
            Assert.True(found.Result);
            Assert.False(fakeNotFound.Result);
            
            /*Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            var vm = viewResult!.Model;
            Assert.IsType<TestViewModel>(vm);
            var testVm = vm as TestViewModel;
            Assert.NotNull(testVm!.Persons);
            // for debugging
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            Assert.Equal(0, testVm.Persons.Count);*/
        }

        [Fact]
        public async Task Action_Test_Update()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            var result = await _bll.Persons.GetAllAsync();
            
            person.FirstName = "Updated";

            _ctx.ChangeTracker.Clear();
           
            _service.Update(person); // had to comment out PersonRepository translation
            await _bll.SaveChangesAsync();

            var found = await _service.FirstOrDefaultAsync(id);
            

            // ASSERT

            Assert.Equal("Updated", found!.FirstName);
           
        }
        
        [Fact]
        public async Task Action_Test_Remove()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            
            var result1 = await _bll.Persons.GetAllAsync();

            _ctx.ChangeTracker.Clear();
           
            _service.Remove(person);
            await _bll.SaveChangesAsync();

            var result2 = await _bll.Persons.GetAllAsync();
            

            // ASSERT
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 0);
            
        }
        
        [Fact]
        public async Task Action_Test_GetAllAsync()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
            var id2 = Guid.NewGuid();
            var person2 = new BLL.App.DTO.Person()
            {
                Id = id2,
                FirstName = "FFF",
                LastName = "444",
                PersonsIdCode = "323214"
            };
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            
            var result1 = await _service.GetAllAsync();

            _ctx.ChangeTracker.Clear();
           
            _service.Add(person2); 
            await _bll.SaveChangesAsync();

            var result2 = await _service.GetAllAsync();
            

            // ASSERT
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 2);
            
        }
        
        [Fact]
        public async Task Action_Test_FirstOrDefaultAsync()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
           
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            
            var result = await _service.FirstOrDefaultAsync(id);

            
            

            // ASSERT
            Assert.Equal("AAA", result!.FirstName);
        }
        
        [Fact]
        public async Task Action_Test_ExistsAsync()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
           
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            
            var fakeId = Guid.NewGuid();
            
            var found = await _service.ExistsAsync(id);
            var notFound = await _service.ExistsAsync(fakeId);

            // ASSERT
            Assert.True(found);
            Assert.False(notFound);
        }
        
        [Fact]
        public async Task Action_Test_RemoveAsync()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new BLL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            
            // ARRANGE
            _service.Add(person);
           
            await _bll.SaveChangesAsync();
            
            var result1 = await _bll.Persons.GetAllAsync();

            _ctx.ChangeTracker.Clear();
           
            await _service.RemoveAsync(person.Id);
            await _bll.SaveChangesAsync();

            var result2 = await _bll.Persons.GetAllAsync();
            

            // ASSERT
            Assert.True(result1.Count() == 1);
            Assert.True(result2.Count() == 0);
        }
    }
}