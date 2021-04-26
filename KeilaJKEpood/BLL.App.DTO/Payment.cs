using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Payment : DomainEntityId
    {
    
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PaymentTypeId")]
        public Guid PaymentTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "BillId")]
        public Guid BillId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PersonId")]
        public Guid PersonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PaymentTime")]
        public DateTime PaymentTime { get; set; } // When was the bill payed for
    }
}