using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class DiscountMapper: BaseMapper<BLL.App.DTO.Discount, DAL.App.DTO.Discount>, IBaseMapper<BLL.App.DTO.Discount, DAL.App.DTO.Discount>
    {
        public DiscountMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}