using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class LineOnBill
    {
        public Guid Id { get; set; }
        
        public Guid BillId { get; set; }
        
        public Guid PriceId { get; set; }
        
        public Guid ProductId { get; set; }
        
        public int Amount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal TaxPercentage { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceWithoutTax { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal SumOfTax { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceToPay { get; set; }
    }
}