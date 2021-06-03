using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class WarehouseDTO
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Address")]
        [MaxLength(64)]
        public string Address { get; set; }  = default!;
        
        public Guid PhoneId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Phone")]
        [MaxLength(64)]
        public string Phone { get; set; }  = default!;
        
        public Guid EmailId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Email")]
        public string Email { get; set; }  = default!;
        
        public Guid WarehouseCodeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "WarehouseCode")]
        public string WarehouseCode { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "From")]
        public DateTime From { get; set; } // When was the warehouse added
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Warehouse), Name = "Until")]
        public DateTime? Until { get; set; } // When the warehouse was closed
    }
}