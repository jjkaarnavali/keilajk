using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.App.Services;
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
    public class PersonServiceUnitTests
    {
        private readonly TestController _testController;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;


        private readonly BaseEntityService<IAppUnitOfWork, IPersonRepository, BLL.App.DTO.Person, DAL.App.DTO.Person>
            _service;

        private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        public PersonServiceUnitTests(ITestOutputHelper testOutputHelper)
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
            

            _service =
                new PersonService(_uow, _uow.Persons, _mapper);

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();

            // SUT
            _testController = new TestController(logger, _ctx);

            
        }
        [Fact]
        public async Task Action_Test_GetAllNewAsync()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new DAL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            _uow.Persons.Add(person);
            await _bll.SaveChangesAsync();

            
            
            var result = await _bll.Persons.GetAllNewAsync(id);

            // ASSERT
            
            Assert.True(result.Count() == 1);
        }
        
        [Fact]
        public async Task Action_Test_GetAllPersonsWithInfo()
        {
            // ACT
            var id = Guid.NewGuid();
            var person = new DAL.App.DTO.Person()
            {
                Id = id,
                FirstName = "AAA",
                LastName = "BBB",
                PersonsIdCode = "123214"
            };
            _uow.Persons.Add(person);
            await _bll.SaveChangesAsync();

            
            
            var result = await _bll.Persons.GetAllPersonsWithInfo(id);

            // ASSERT
            
            Assert.True(result.Count() == 1);
        }
        
    }
}