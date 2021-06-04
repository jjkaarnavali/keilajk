using System.Collections;
using System.Collections.Generic;
using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class QuizDetailsViewModel
    {
        public Quiz? Quiz { get; set; }
        public IEnumerable<QuestionViewModel>? Questions { get; set; } = new List<QuestionViewModel>();
    }
}