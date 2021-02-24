using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Price
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Guid DiscountId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceInEur { get; set; }
        
        public DateTime From { get; set; } // When was the price added
        
        public DateTime? Until { get; set; } // When was the price changed or product removed
    }
}