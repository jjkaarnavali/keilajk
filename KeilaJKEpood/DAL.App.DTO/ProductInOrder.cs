using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ProductInOrder : DomainEntityId
    {
     
        
        public Guid ProductId { get; set; }
        
        public Guid OrderId { get; set; }  = default!;
        
        public int ProductAmount { get; set; }
        
        public DateTime From { get; set; } // When was the product added to the order
        
        public DateTime? Until { get; set; } // When was the product removed from the order
    }
}