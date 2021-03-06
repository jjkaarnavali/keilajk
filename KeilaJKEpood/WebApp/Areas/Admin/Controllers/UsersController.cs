using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable 1591


namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        
        

        public UsersController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        

        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
       

        public async Task<IActionResult> Details(Guid? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }
            
            var userRoles = await _userManager.GetRolesAsync(appUser);
            
            
            

            

            return View(appUser);
        }
        public async Task<ViewResult> Roles(Guid? id)
        {
            
            
            

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            
            
            var userRoles = await _userManager.GetRolesAsync(appUser);
            List<String> roles = new List<string>();
            for (int i = 0; i < userRoles.Count; i++)
            {
                roles.Add(userRoles[i]);
            }
            roles.Add(appUser.Id.ToString());
            
            

            

            return View(roles);
        }
        
        

        

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Block(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            await _userManager.SetLockoutEnabledAsync(appUser, true);
            await _userManager.SetLockoutEndDateAsync(appUser, DateTimeOffset.MaxValue);
            

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UnBlock(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            await _userManager.SetLockoutEnabledAsync(appUser, true);
            await _userManager.SetLockoutEndDateAsync(appUser, DateTimeOffset.MinValue);
            

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> BlockOrUnblock(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            
            

            return View(appUser);
        }
        
        public async Task<IActionResult> AddToCustomerRole(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            await _userManager.AddToRoleAsync(appUser, "customer");
            
            

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> RemoveFromRole(Guid? id, string role)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            await _userManager.RemoveFromRoleAsync(appUser, role.ToUpper());
            
            

            return RedirectToAction(nameof(Index));
        }
    }
    
    
}
