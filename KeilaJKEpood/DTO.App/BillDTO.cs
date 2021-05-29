using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class BillUpdate
    {
        
        [MaxLength(64)] 
        public string Id { get; set; } = default!;
        
        [MaxLength(64)] 
        public string PersonId { get; set; } = default!;

        [MaxLength(64)] 
        public string UserId { get; set; } = default!;
        
        public string OrderId { get; set; }  = default!;
        
        public Decimal PriceWithoutTax { get; set; }  = default!;
        
        public Decimal SumOfTax { get; set; }  = default!;
        
        public Decimal PriceToPay { get; set; }  = default!;
    }
    public class BillAdd
    {
        [MaxLength(64)] 
        public string PersonId { get; set; } = default!;

        [MaxLength(64)] 
        public string UserId { get; set; } = default!;
        
        public string OrderId { get; set; }  = default!;
        
        public Decimal PriceWithoutTax { get; set; }  = default!;
        
        public Decimal SumOfTax { get; set; }  = default!;
        
        public Decimal PriceToPay { get; set; }  = default!;
    }
}