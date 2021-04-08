using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class LineOnBillMapper : BaseMapper<DAL.App.DTO.LineOnBill, Domain.App.LineOnBill>,  IBaseMapper<DAL.App.DTO.LineOnBill, Domain.App.LineOnBill>
    {
        public LineOnBillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}