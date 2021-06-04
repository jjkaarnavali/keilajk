using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class QuestionViewModel
    {
        public Question? Question { get; set; }
        public SelectList? Answers { get; set; }
    }
}