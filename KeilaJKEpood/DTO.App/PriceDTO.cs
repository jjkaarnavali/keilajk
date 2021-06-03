using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.App
{
    public class PriceDTO
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Price), Name = "ProductId")]
        public Guid ProductId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Price), Name = "DiscountId")]
        public Guid DiscountId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Price), Name = "PriceInEur")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceInEur { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Price), Name = "From")]
        public DateTime From { get; set; } // When was the price added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Price), Name = "Until")]
        public DateTime? Until { get; set; } // When was the price changed or product removed
    }
}