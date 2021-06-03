using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PaymentTypeId")]
        public Guid PaymentTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "BillId")]
        public Guid BillId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PersonId")]
        public Guid PersonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Payment), Name = "PaymentTime")]
        public DateTime PaymentTime { get; set; } // When was the bill payed for
    }
    public class PaymentAdd
    {
        [MaxLength(64)] 
        public string Id { get; set; } = default!;

        [MaxLength(64)] 
        public string PaymentTypeId { get; set; } = default!;
        
        public string BillId { get; set; } = default!;
        
        public string PersonId { get; set; } = default!;
    }
}