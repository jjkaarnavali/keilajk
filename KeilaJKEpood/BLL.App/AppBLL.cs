using System;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }


        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(Uow, Uow.Persons, Mapper));

        /*public IContactService Contacts =>
            GetService<IContactService>(() => new ContactService(Uow, Uow.Contacts));

        public IContactTypeService ContactTypes =>
            GetService<IContactTypeService>(() => new ContactTypeService(Uow, Uow.ContactTypes));*/

        /*public IBaseEntityService<BLL.App.DTO.Discount, DAL.App.DTO.Simple> Discounts =>
            GetService<IBaseEntityService<BLL.App.DTO.Discount, DAL.App.DTO.Discount>>(()
                => new BaseEntityService<IAppUnitOfWork,
                    IBaseRepository<DAL.App.DTO.Discount>, BLL.App.DTO.Discount, DAL.App.DTO.Discount>(Uow, Uow.Discounts, new BaseMapper<BLL.App.DTO.Discount, DAL.App.DTO.Discount>(Mapper)));*/
    }
}

  