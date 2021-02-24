using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductInOrder
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public string OrderId { get; set; }  = default!;
        
        public int ProductAmount { get; set; }
        
        public DateTime From { get; set; } // When was the product added to the order
        
        public DateTime? Until { get; set; } // When was the product removed from the order
    }
}