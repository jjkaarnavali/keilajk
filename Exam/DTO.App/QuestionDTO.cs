using System;

namespace DTO.App
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string QuestionText { get; set; } = default!;
    }
}