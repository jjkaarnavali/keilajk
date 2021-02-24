using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string Address { get; set; }  = default!;
        
        [MaxLength(64)]
        public string Phone { get; set; }  = default!;
        
        public string Email { get; set; }  = default!;
        
        public string WarehouseCode { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the warehouse added
        
        public DateTime? Until { get; set; } // When the warehouse was closed

    }
}