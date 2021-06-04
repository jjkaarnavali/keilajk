using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class QuestionCreateEditViewModel
    {
        public Question? Question { get; set; }
        public SelectList? QuizSelectList { get; set; }
    }
}