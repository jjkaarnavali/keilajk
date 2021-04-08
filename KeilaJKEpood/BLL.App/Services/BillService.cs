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
    public class BillService: BaseEntityService<IAppUnitOfWork, IBillRepository, BLLAppDTO.Bill, DALAppDTO.Bill>, IBillService
    {
        public BillService(IAppUnitOfWork serviceUow, IBillRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new BillMapper(mapper))
        {
        }
    }
}