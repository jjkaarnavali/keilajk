using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class CompanyMapper: BaseMapper<BLL.App.DTO.Company, DAL.App.DTO.Company>, IBaseMapper<BLL.App.DTO.Company, DAL.App.DTO.Company>
    {
        public CompanyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}