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
    public class PaymentService: BaseEntityService<IAppUnitOfWork, IPaymentRepository, BLLAppDTO.Payment, DALAppDTO.Payment>, IPaymentService
    {
        public PaymentService(IAppUnitOfWork serviceUow, IPaymentRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PaymentMapper(mapper))
        {
        }
    }
}