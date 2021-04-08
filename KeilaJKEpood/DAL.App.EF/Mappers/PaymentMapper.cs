using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PaymentMapper : BaseMapper<DAL.App.DTO.Payment, Domain.App.Payment>,  IBaseMapper<DAL.App.DTO.Payment, Domain.App.Payment>
    {
        public PaymentMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}