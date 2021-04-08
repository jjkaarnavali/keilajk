using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductTypeMapper : BaseMapper<DAL.App.DTO.ProductType, Domain.App.ProductType>,  IBaseMapper<DAL.App.DTO.ProductType, Domain.App.ProductType>
    {
        public ProductTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}