using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class LineOnBillDTO
    {
    }
    public class LineOnBillAdd
    {
        [MaxLength(64)] 
        public string BillId { get; set; } = default!;

        [MaxLength(64)] 
        public string PriceId { get; set; } = default!;
        
        public string ProductId { get; set; }  = default!;
        
        public int Amount { get; set; }  = default!;
        
        public Decimal TaxPercentage { get; set; }  = default!;
        
        public Decimal PriceWithoutTax { get; set; }  = default!;
        
        public Decimal SumOfTax { get; set; }  = default!;
        
        public Decimal PriceToPay { get; set; }  = default!;
    }
}