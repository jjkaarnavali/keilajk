using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductInOrderMapper : BaseMapper<DAL.App.DTO.ProductInOrder, Domain.App.ProductInOrder>,  IBaseMapper<DAL.App.DTO.ProductInOrder, Domain.App.ProductInOrder>
    {
        public ProductInOrderMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}