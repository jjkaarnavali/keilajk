using System.Collections.Generic;
using Domain.App;

namespace WebApp.Models
{
    public class GameViewModel
    {
        public Game? Game { get; set; }
        public Quiz? Quiz { get; set; }
        public IEnumerable<Question>? QuizQuestions { get; set; }
    }
}