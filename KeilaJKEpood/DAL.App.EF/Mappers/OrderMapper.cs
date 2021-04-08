using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OrderMapper : BaseMapper<DAL.App.DTO.Order, Domain.App.Order>,  IBaseMapper<DAL.App.DTO.Order, Domain.App.Order>
    {
        public OrderMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}