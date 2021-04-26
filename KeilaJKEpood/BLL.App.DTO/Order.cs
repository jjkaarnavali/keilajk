using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Order : DomainEntityId
    {
    
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Order), Name = "UserId")]
        public Guid UserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Order), Name = "From")]
        public DateTime From { get; set; } // When was the order added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Order), Name = "Until")]
        public DateTime? Until { get; set; } // When the order was canceled

    }
}