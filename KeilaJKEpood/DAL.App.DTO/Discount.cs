using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Discount : DomainEntityId
    {
     
        public Guid RequiredUserLevelId { get; set; }
        public string? RequiredUserLevel { get; set; } // Null if discount is for everyone, even those without accounts
        
        public Guid DiscountPercentageId { get; set; }
        public string DiscountPercentage { get; set; }  = default!;
    
        public Guid DiscountNameId { get; set; }
        public string DiscountName { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the warehouse added
        
        public DateTime? Until { get; set; } // When the warehouse was closed
    }
}