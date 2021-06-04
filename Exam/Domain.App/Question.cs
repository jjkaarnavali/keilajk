using System;

namespace Domain.App
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string QuestionText { get; set; } = default!;
    }
}