using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ScorePageController : Controller
    {
        private readonly AppDbContext _context;

        public ScorePageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ScorePage/Index/id
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);
            var quiz = await _context.Quizzes.FindAsync(game.QuizId);

            var vm = new QuizGameViewModel
            {
                Game = game,
                Quiz = quiz,
                QuizQuestions = _context.Questions.Where(q => q.QuizId.Equals(game.QuizId))
            };
            
            if (vm.Game == null)
            {
                return NotFound();
            }

            return View(vm);
        }

    }
}