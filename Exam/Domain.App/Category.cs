using System;

namespace Domain.App
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = default!; // Either Poll or Quiz
    }
}