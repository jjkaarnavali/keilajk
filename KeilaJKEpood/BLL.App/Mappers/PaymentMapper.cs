using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PaymentMapper: BaseMapper<BLL.App.DTO.Payment, DAL.App.DTO.Payment>, IBaseMapper<BLL.App.DTO.Payment, DAL.App.DTO.Payment>
    {
        public PaymentMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}