using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PriceService: BaseEntityService<IAppUnitOfWork, IPriceRepository, BLLAppDTO.Price, DALAppDTO.Price>, IPriceService
    {
        public PriceService(IAppUnitOfWork serviceUow, IPriceRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PriceMapper(mapper))
        {
        }
    }
}