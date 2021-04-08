using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class LineOnBillMapper: BaseMapper<BLL.App.DTO.LineOnBill, DAL.App.DTO.LineOnBill>, IBaseMapper<BLL.App.DTO.LineOnBill, DAL.App.DTO.LineOnBill>
    {
        public LineOnBillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}