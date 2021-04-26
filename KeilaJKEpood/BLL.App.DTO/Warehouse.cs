using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Warehouse : DomainEntityId
    {
       
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Address")]
        [MaxLength(64)]
        public string Address { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Phone")]
        [MaxLength(64)]
        public string Phone { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Email")]
        public string Email { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "WarehouseCode")]
        public string WarehouseCode { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "From")]
        public DateTime From { get; set; } // When was the warehouse added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Until")]
        public DateTime? Until { get; set; } // When the warehouse was closed

    }
}