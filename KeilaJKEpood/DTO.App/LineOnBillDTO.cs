using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.App
{
    public class LineOnBillDTO
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "BillId")]
        public Guid BillId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "PriceId")]
        public Guid PriceId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "ProductId")]
        public Guid ProductId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "Amount")]
        public int Amount { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "TaxPercentage")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal TaxPercentage { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "PriceWithoutTax")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceWithoutTax { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "SumOfTax")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal SumOfTax { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LineOnBill), Name = "PriceToPay")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceToPay { get; set; }
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