using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class Bill : DomainEntityId
    {

        public Guid PersonId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid OrderId { get; set; }
        
        public string BillNr { get; set; }  = default!;
        
        public DateTime CreationTime { get; set; } // When was the bill generated
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceWithoutTax { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal SumOfTax { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceToPay { get; set; }
    }
}