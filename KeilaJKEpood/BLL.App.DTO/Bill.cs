using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Bill : DomainEntityId
    {

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "PersonId")]
        public Guid PersonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "UserId")]
        public Guid UserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Bill), Name = "OrderId")]
        public Guid OrderId { get; set; }
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
}