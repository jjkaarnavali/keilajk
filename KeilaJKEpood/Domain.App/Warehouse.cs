using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Warehouse : DomainEntityId
    {
       
        public Guid AddressId { get; set; }
        [MaxLength(64)]
        public LangString? Address { get; set; }  = default!;
        
        public Guid PhoneId { get; set; }
        [MaxLength(64)]
        public LangString? Phone { get; set; }  = default!;
        
        public Guid EmailId { get; set; }
        public LangString? Email { get; set; }  = default!;
        
        public Guid WarehouseCodeId { get; set; }
        public LangString? WarehouseCode { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the warehouse added
        
        public DateTime? Until { get; set; } // When the warehouse was closed

    }
}