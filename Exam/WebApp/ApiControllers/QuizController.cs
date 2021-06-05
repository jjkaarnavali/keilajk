using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Quiz = DTO.App.QuizDTO;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            var dtoQuizzes = new List<Quiz>();
            foreach (var quiz in quizzes)
            {
                var dtoQuiz = new Quiz()
                {
                    Id = quiz.Id,
                    AverageScore = quiz.AverageScore,
                    CategoryId = quiz.CategoryId,
                    CategoryName = quiz.CategoryName,
                    Description = quiz.Description,
                    Name = quiz.Name,
                    TimesPlayed = quiz.TimesPlayed
                };
                dtoQuizzes.Add(dtoQuiz);
            }
            return dtoQuizzes;
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }
            var dtoQuiz = new Quiz()
            {
                Id = quiz.Id,
                AverageScore = quiz.AverageScore,
                CategoryId = quiz.CategoryId,
                CategoryName = quiz.CategoryName,
                Description = quiz.Description,
                Name = quiz.Name,
                TimesPlayed = quiz.TimesPlayed
            };

            return dtoQuiz;
        }

        // PUT: api/Quiz/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quiz
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            var domainQuiz = new Domain.App.Quiz()
            {
                Id = quiz.Id,
                AverageScore = quiz.AverageScore,
                CategoryId = quiz.CategoryId,
                CategoryName = quiz.CategoryName,
                Description = quiz.Description,
                Name = quiz.Name,
                TimesPlayed = quiz.TimesPlayed
            };
            _context.Quizzes.Add(domainQuiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(Guid id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
