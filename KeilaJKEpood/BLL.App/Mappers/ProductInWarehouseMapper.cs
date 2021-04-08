using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductInWarehouseMapper: BaseMapper<BLL.App.DTO.ProductInWarehouse, DAL.App.DTO.ProductInWarehouse>, IBaseMapper<BLL.App.DTO.ProductInWarehouse, DAL.App.DTO.ProductInWarehouse>
    {
        public ProductInWarehouseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}