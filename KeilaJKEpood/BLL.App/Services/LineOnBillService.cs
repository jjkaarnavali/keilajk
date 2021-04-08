﻿using System;
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
    public class LineOnBillService: BaseEntityService<IAppUnitOfWork, ILineOnBillRepository, BLLAppDTO.LineOnBill, DALAppDTO.LineOnBill>, ILineOnBillService
    {
        public LineOnBillService(IAppUnitOfWork serviceUow, ILineOnBillRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LineOnBillMapper(mapper))
        {
        }
    }
}