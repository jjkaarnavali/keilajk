using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Answer = DTO.App.AnswerDTO;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[ApiVersion("1.0")]
    public class AnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Answer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var answers = await _context.Answers.ToListAsync();
            var dtoAnswers = new List<Answer>();
            foreach (var answer in answers)
            {
                var dtoAnswer = new Answer()
                {
                    Id = answer.Id,
                    QuestionId = answer.QuestionId,
                    AnswerText = answer.AnswerText,
                    IsCorrect = answer.IsCorrect
                };
                dtoAnswers.Add(dtoAnswer);
            }
            return dtoAnswers;
        }

        // GET: api/Answer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }
            var dtoAnswer = new Answer()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect
            };

            return dtoAnswer;
        }

        // PUT: api/Answer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }
            var domainAnswer = new Domain.App.Answer()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect
            };

            _context.Entry(domainAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            var domainAnswer = new Domain.App.Answer()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                IsCorrect = answer.IsCorrect
            };
            _context.Answers.Add(domainAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(Guid id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
