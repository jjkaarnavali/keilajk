using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.App
{
    
    public class BillDTO
    {
        public Guid Id { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "PersonId")]
        public Guid PersonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "UserId")]
        public Guid UserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "OrderId")]
        public Guid OrderId { get; set; }
        
        public Guid BillNrId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "BillNr")]
        public string BillNr { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "CreationTime")]
        public DateTime CreationTime { get; set; } // When was the bill generated
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "PriceWithoutTax")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceWithoutTax { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "SumOfTax")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal SumOfTax { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "PriceToPay")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal PriceToPay { get; set; }
    }
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