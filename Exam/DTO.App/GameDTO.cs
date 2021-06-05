using System;

namespace DTO.App
{
    public class GameDTO
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public int Score { get; set; }
    }
}