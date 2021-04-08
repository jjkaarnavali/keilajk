using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class BillMapper: BaseMapper<BLL.App.DTO.Bill, DAL.App.DTO.Bill>, IBaseMapper<BLL.App.DTO.Bill, DAL.App.DTO.Bill>
    {
        public BillMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}