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
    public class ProductService: BaseEntityService<IAppUnitOfWork, IProductRepository, BLLAppDTO.Product, DALAppDTO.Product>, IProductService
    {
        public ProductService(IAppUnitOfWork serviceUow, IProductRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProductMapper(mapper))
        {
            
        }
    }
    
}