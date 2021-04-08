using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DiscountMapper : BaseMapper<DAL.App.DTO.Discount, Domain.App.Discount>,  IBaseMapper<DAL.App.DTO.Discount, Domain.App.Discount>
    {
        public DiscountMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}