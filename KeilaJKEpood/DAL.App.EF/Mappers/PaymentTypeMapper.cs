using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PaymentTypeMapper : BaseMapper<DAL.App.DTO.PaymentType, Domain.App.PaymentType>,  IBaseMapper<DAL.App.DTO.PaymentType, Domain.App.PaymentType>
    {
        public PaymentTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}