using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PriceMapper: BaseMapper<BLL.App.DTO.Price, DAL.App.DTO.Price>, IBaseMapper<BLL.App.DTO.Price, DAL.App.DTO.Price>
    {
        public PriceMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}