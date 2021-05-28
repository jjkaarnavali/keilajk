using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class OrderDTO
    {   
        public Guid Id { get; set; }
        
        [MaxLength(64)] public Guid userId { get; set; } = default!;

        [MaxLength(64)] public DateTime? LastName { get; set; } = default!;
        
        public string PersonsIdCode { get; set; }  = default!;
        
        
    }
    public class OrderAdd
    {
        [MaxLength(64)] 
        public string id { get; set; } = default!;

        [MaxLength(64)] 
        public string userId { get; set; } = default!;
        
        public string? until { get; set; }  = default!;
    }
}