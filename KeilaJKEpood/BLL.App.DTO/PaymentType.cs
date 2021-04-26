using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class PaymentType : DomainEntityId
    {
       
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PaymentType), Name = "PaymentTypeName")]
        [MaxLength(32)]
        public string? PaymentTypeName { get; set; } // Through bank, credit/debit card, paypal
    }
}