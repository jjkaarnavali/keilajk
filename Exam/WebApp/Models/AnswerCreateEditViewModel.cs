using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class AnswerCreateEditViewModel
    {
        public Answer? Answer { get; set; }
        public SelectList? QuestionSelectList { get; set; }
    }
}