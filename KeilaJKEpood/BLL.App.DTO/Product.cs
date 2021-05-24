using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Product : DomainEntityId
    {
    
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "CompanyId")]
        public Guid CompanyId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "ProductTypeId")]
        public Guid ProductTypeId { get; set; }
        
        
        public Guid ProductNameId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "ProductName")]
        public string ProductName { get; set; }  = default!;
        
        
        public Guid ProductSizeId { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "ProductSize")]
        public string ProductSize { get; set; }  = default!;
        
        
        public Guid ProductSeasonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "ProductSeason")]
        public string ProductSeason { get; set; }  = default!;
        
        
        public Guid ProductCodeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "ProductCode")]
        public string ProductCode { get; set; }  = default!;
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "From")]
        public DateTime From { get; set; } // When was the product added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Product), Name = "Until")]
        public DateTime? Until { get; set; } // When the product was removed
        
        public decimal Price { get; set; }  = default!;

    }
    
}

