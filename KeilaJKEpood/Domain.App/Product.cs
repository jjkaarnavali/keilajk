using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Product : DomainEntityId
    {
    
        
        public Guid CompanyId { get; set; }
        
        public Guid ProductTypeId { get; set; }
        
        
        public string ProductName { get; set; }  = default!;
        
        public string ProductSize { get; set; }  = default!;
        
        public string ProductSeason { get; set; }  = default!;
        
        public string ProductCode { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the product added
        
        public DateTime? Until { get; set; } // When the product was removed

    }
}