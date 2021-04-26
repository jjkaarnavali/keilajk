using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Discount : DomainEntityId
    {
     
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Discount), Name = "RequiredUserLevel")]
        public string? RequiredUserLevel { get; set; } // Null if discount is for everyone, even those without accounts
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Discount), Name = "DiscountPercentage")]
        public string DiscountPercentage { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Discount), Name = "DiscountName")]
        public string DiscountName { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Discount), Name = "From")]
        public DateTime From { get; set; } // When was the warehouse added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Discount), Name = "Until")]
        public DateTime? Until { get; set; } // When the warehouse was closed
    }
}