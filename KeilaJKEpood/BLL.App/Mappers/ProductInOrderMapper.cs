using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductInOrderMapper: BaseMapper<BLL.App.DTO.ProductInOrder, DAL.App.DTO.ProductInOrder>, IBaseMapper<BLL.App.DTO.ProductInOrder, DAL.App.DTO.ProductInOrder>
    {
        public ProductInOrderMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}