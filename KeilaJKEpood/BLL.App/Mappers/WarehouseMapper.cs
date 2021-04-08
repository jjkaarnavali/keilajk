using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class WarehouseMapper: BaseMapper<BLL.App.DTO.Warehouse, DAL.App.DTO.Warehouse>, IBaseMapper<BLL.App.DTO.Warehouse, DAL.App.DTO.Warehouse>
    {
        public WarehouseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}