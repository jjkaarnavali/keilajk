using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Order : DomainEntityId
    {
    
        
        public Guid UserId { get; set; }
        
        public DateTime From { get; set; } // When was the order added
        
        public DateTime? Until { get; set; } // When the order was canceled

    }
}