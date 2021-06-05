using System;

namespace DTO.App
{
    public class AnswerDTO
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string AnswerText { get; set; } = default!;
        public bool IsCorrect { get; set; } = false;
    }
}