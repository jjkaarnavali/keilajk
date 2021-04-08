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
    public class PaymentTypeService: BaseEntityService<IAppUnitOfWork, IPaymentTypeRepository, BLLAppDTO.PaymentType, DALAppDTO.PaymentType>, IPaymentTypeService
    {
        public PaymentTypeService(IAppUnitOfWork serviceUow, IPaymentTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PaymentTypeMapper(mapper))
        {
        }
    }
}