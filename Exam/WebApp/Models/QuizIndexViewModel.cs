using System.Collections.Generic;
using Domain.App;

namespace WebApp.Models
{
    public class QuizIndexViewModel
    {
        public IEnumerable<Quiz>? Quizzes { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}