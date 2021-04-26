using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductInWarehouse : DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInWarehouse), Name = "ProductId")]
        public Guid ProductId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInWarehouse), Name = "WarehouseId")]
        public Guid WarehouseId { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInWarehouse), Name = "ProductAmount")]
        public int ProductAmount { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInWarehouse), Name = "From")]
        public DateTime From { get; set; } // When was the product added to the order
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInWarehouse), Name = "Until")]
        public DateTime? Until { get; set; } // When was the product removed from the order
    }
}