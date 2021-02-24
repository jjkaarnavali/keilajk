using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        
        public DateTime From { get; set; } // When was the order added
        
        public DateTime? Until { get; set; } // When the order was canceled

    }
}