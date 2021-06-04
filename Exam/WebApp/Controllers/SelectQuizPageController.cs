using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SelectQuizPageController : Controller
    {
        private readonly AppDbContext _context;

        public SelectQuizPageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SelectQuizPage/Index
        public async Task<IActionResult> Index(string? search)
        {
            var vm = new QuizIndexViewModel
            {
                Quizzes = await _context.Quizzes.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            if (!string.IsNullOrWhiteSpace(search))
            {
                vm.Quizzes = vm.Quizzes.Where(quiz =>
                    quiz.CategoryId.ToString().Equals(search));
            }

            return View(vm);
        }

        // GET: SelectQuizPage/Play/id
        public async Task<IActionResult> Play(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var correctQuestions = await _context.Questions.Where(q => q.QuizId.Equals(id)).ToListAsync();
            var questions = new List<QuestionViewModel>();
            foreach (var question in correctQuestions)
            {
                var answers = new SelectList(_context.Answers.Where(a => a.QuestionId.Equals(question.Id)),
                    nameof(Answer.Id), nameof(Answer.AnswerText));
                var question1 = new QuestionViewModel{};
                question1.Question = question;
                question1.Answers = answers;
                questions.Add(question1);
            }
            var vm = new QuizDetailsViewModel
            {
                Quiz = await _context.Quizzes
                    .FirstOrDefaultAsync(m => m.Id == id),
                Questions = questions
            };

            

            if (vm.Quiz == null)
            {
                return NotFound();
            }

            return View(vm);
        }


        public async Task<IActionResult> PlayQuiz(Guid id, IFormCollection answers)
        {
            var correctAnswers = 0;
            var totalAnswers = 0;
            foreach (var answer in answers.Take(answers.Count - 1)) // don't count last one, because it's verification
            {
                foreach (var answerId in answer.Value.ToString().Split(","))
                {
                    if (_context.Answers.FindAsync(Guid.Parse(answerId)).Result.IsCorrect)
                    {
                        correctAnswers++;
                    }
                    
                    totalAnswers++;
                }
            }

            var score = (int) (((double) correctAnswers / (double) totalAnswers) * 100);
            var playedQuiz = _context.Quizzes.FindAsync(id).Result;
            playedQuiz.AverageScore = (playedQuiz.AverageScore * playedQuiz.TimesPlayed + score) / (playedQuiz.TimesPlayed + 1);
            playedQuiz.TimesPlayed++;
            _context.Quizzes.Update(playedQuiz);
            await _context.SaveChangesAsync();
            
            var game = new Game
            {
                QuizId = id,
                Score = score
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "ScorePage", new { id = game.Id });
        }

    }
}