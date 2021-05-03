using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Discount : DomainEntityId
    {
     
        public Guid RequiredUserLevelId { get; set; }
        public LangString? RequiredUserLevel { get; set; } // Null if discount is for everyone, even those without accounts
        
        public Guid DiscountPercentageId { get; set; }
        public LangString? DiscountPercentage { get; set; }
    
        public Guid DiscountNameId { get; set; }
        public LangString? DiscountName { get; set; }
        
        public DateTime From { get; set; } // When was the warehouse added
        
        public DateTime? Until { get; set; } // When the warehouse was closed
    }
}