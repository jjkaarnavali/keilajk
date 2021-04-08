using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class BillMapper : BaseMapper<DAL.App.DTO.Bill, Domain.App.Bill>,  IBaseMapper<DAL.App.DTO.Bill, Domain.App.Bill>
    {
        public BillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}