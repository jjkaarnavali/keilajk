using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Product : DomainEntityId
    {
    
        
        public Guid CompanyId { get; set; }
        
        public Guid ProductTypeId { get; set; }
        
        public Guid ProductNameId { get; set; }
        public LangString? ProductName { get; set; }
        
        public Guid ProductSizeId { get; set; }
        public LangString? ProductSize { get; set; }
        
        public Guid ProductSeasonId { get; set; }
        public LangString? ProductSeason { get; set; }
        
        public Guid ProductCodeId { get; set; }
        public LangString? ProductCode { get; set; }
        
        public DateTime From { get; set; } // When was the product added
        
        public DateTime? Until { get; set; } // When the product was removed

    }
}