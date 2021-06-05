using System;

namespace DTO.App
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = default!; // Either Poll or Quiz
    }
}