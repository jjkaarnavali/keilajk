using System;

namespace Domain.App
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public int Score { get; set; }
    }
}