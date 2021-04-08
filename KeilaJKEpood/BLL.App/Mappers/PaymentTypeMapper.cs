using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PaymentTypeMapper: BaseMapper<BLL.App.DTO.PaymentType, DAL.App.DTO.PaymentType>, IBaseMapper<BLL.App.DTO.PaymentType, DAL.App.DTO.PaymentType>
    {
        public PaymentTypeMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}