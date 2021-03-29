using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        
        //private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public PersonsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.Persons.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                person.AppUserId = User.GetUserId()!.Value;
                _uow.Persons.Add(person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid || !await _uow.Persons.ExistsAsync(person.Id, User.GetUserId()!.Value))
                return View(person);

            person.AppUserId = User.GetUserId()!.Value;
            _uow.Persons.Update(person);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Persons.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
    }
}
