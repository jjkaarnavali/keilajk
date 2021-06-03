using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Person = DTO.App.PersonDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Persons
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PersonsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
        }

        // GET: api/Persons
        /// <summary>
        /// Get all Persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DTO.App.PersonDTO>>> GetPersons()
        {
            return Ok(await _bll.Persons.GetAllAsync());
        }

        // GET: api/Persons/5
        /// <summary>
        /// Get one Person. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.Person</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DTO.App.PersonDTO>> GetPerson(Guid id)
        {

            var persons = await _bll.Persons.GetAllAsync();
            var person = await _bll.Persons.FirstOrDefaultAsync(id);

            foreach (var per in persons)
            {
                if (per.Id == id)
                {
                    person = per;
                } 
            }
            
            
            if (person == null)
            {
                return NotFound();
            }

            
            return Mapper.Map(person, new DTO.App.PersonDTO());
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Person thats already in the DB
        /// </summary>
        /// <param name="id">Id of the Person</param>
        /// <param name="person">The updated Person</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }
            
            var persons = await _bll.Persons.GetAllAsync();
            var perso = await _bll.Persons.FirstOrDefaultAsync(id);

            foreach (var per in persons)
            {
                if (per.Id == id)
                {
                    perso = per;
                } 
            }

            person.AppUserId = perso!.AppUserId;
            

            
            
            _bll.Persons.Update(Mapper.Map(person, new BLL.App.DTO.Person()));
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Person
        /// </summary>
        /// <param name="person">Entity of type BLL.App.DTO.Person</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DTO.App.PersonDTO>> PostPerson(DTO.App.PersonAdd person)
        {
            
            var bllPerson = new BLL.App.DTO.Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonsIdCode = person.PersonsIdCode
            };

            bllPerson.AppUserId = User.GetUserId()!.Value;
            
            var addedPerson = _bll.Persons.Add(bllPerson);
            
            // bll will call dal.SaveChangesAsync => will call EF.SaveChangesAsync()
            // ef will update entities with new ID-s
            await _bll.SaveChangesAsync();

            var returnPerson = new DTO.App.PersonDTO()
            {
                Id = addedPerson.Id,
                FirstName = addedPerson.FirstName,
                LastName = addedPerson.LastName,
                PersonsIdCode = addedPerson.PersonsIdCode
            };

            return CreatedAtAction("GetPerson", new {id = returnPerson.Id}, returnPerson);

        }

        // DELETE: api/Persons/5
        /// <summary>
        /// Delete a Person from the DB.
        /// </summary>
        /// <param name="id">Id of the Person to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var persons = await _bll.Persons.GetAllAsync();
            var userid = User;
            var person = await _bll.Persons.FirstOrDefaultAsync(id);

            foreach (var per in persons)
            {
                if (per.Id == id)
                {
                    person = per;
                } 
            }
            if (person == null)
            {
                return NotFound();
            }

            _bll.Persons.Remove(person);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
