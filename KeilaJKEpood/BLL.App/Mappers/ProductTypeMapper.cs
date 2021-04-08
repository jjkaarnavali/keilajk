using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductTypeMapper: BaseMapper<BLL.App.DTO.ProductType, DAL.App.DTO.ProductType>, IBaseMapper<BLL.App.DTO.ProductType, DAL.App.DTO.ProductType>
    {
        public ProductTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}