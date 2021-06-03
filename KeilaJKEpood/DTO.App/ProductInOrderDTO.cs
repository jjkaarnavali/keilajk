using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class ProductInOrderDTO
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInOrder), Name = "ProductId")]
        public Guid ProductId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInOrder), Name = "OrderId")]
        public Guid OrderId { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInOrder), Name = "ProductAmount")]
        public int ProductAmount { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInOrder), Name = "From")]
        public DateTime From { get; set; } // When was the product added to the order
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductInOrder), Name = "Until")]
        public DateTime? Until { get; set; } // When was the product removed from the order
    }
}