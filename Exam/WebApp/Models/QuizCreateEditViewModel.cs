using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class QuizCreateEditViewModel
    {
        public Quiz? Quiz { get; set; }
        public SelectList? CategorySelectList { get; set; }
    }
}