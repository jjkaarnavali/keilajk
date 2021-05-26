using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PersonService: BaseEntityService<IAppUnitOfWork, IPersonRepository, BLLAppDTO.Person, DALAppDTO.Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork serviceUow, IPersonRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PersonMapper(mapper) )
        {
            
        }

        public async Task<IEnumerable<BLLAppDTO.Person>> GetAllNewAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllNewAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async  Task<IEnumerable<BLLAppDTO.Person>> GetAllPersonsWithInfo(Guid userId)
        {
            var res = (await ServiceUow.Persons
                .GetAllNewAsync(userId)).Select(x => Mapper.Map(x)).ToList();
            
            return res!;
        }
        
    }
}