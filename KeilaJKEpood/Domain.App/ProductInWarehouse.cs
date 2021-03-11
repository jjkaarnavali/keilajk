using System;
using Domain.Base;

namespace Domain.App
{
    public class ProductInWarehouse : DomainEntityId
    {
        public Guid ProductId { get; set; }
        
        public Guid WarehouseId { get; set; }  = default!;
        
        public int ProductAmount { get; set; }
        
        public DateTime From { get; set; } // When was the product added to the order
        
        public DateTime? Until { get; set; } // When was the product removed from the order
    }
}