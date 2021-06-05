using System;

namespace Domain.App
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int AverageScore { get; set; }
        public int TimesPlayed { get; set; }
    }
}