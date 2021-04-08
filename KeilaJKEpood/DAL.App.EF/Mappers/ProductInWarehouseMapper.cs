using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductInWarehouseMapper : BaseMapper<DAL.App.DTO.ProductInWarehouse, Domain.App.ProductInWarehouse>,  IBaseMapper<DAL.App.DTO.ProductInWarehouse, Domain.App.ProductInWarehouse>
    {
        public ProductInWarehouseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}