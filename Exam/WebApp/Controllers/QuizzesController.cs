using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly AppDbContext _context;

        public QuizzesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes.ToListAsync());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            var vm = new QuizCreateEditViewModel
            {
                CategorySelectList = new SelectList(
                    _context.Categories, nameof(Category.Id), nameof(Category.CategoryName))
            };
            return View(vm);
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Name,Description,AverageScore,TimesPlayed")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quiz.Id = Guid.NewGuid();
                var category = await _context.Categories.FirstOrDefaultAsync(q => q.Id == quiz.CategoryId);
                quiz.CategoryName = category.CategoryName;
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var vm = new QuizCreateEditViewModel
            {
                CategorySelectList = new SelectList(
                    _context.Categories, nameof(Category.Id), nameof(Category.CategoryName))
            };
            return View(vm);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new QuizCreateEditViewModel
            {
                CategorySelectList = new SelectList(
                    _context.Categories, nameof(Category.Id), nameof(Category.CategoryName)),
                Quiz = await _context.Quizzes.FindAsync(id)
            };
            
            if (vm.Quiz == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuizCreateEditViewModel vm)
        {
            if (id != vm.Quiz!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(q => q.Id == vm.Quiz.CategoryId);
                    vm.Quiz.CategoryName = category.CategoryName;
                    _context.Update(vm.Quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(vm.Quiz.Id))
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
            return View(vm);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(Guid id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
