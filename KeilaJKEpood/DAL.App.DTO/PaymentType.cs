using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class PaymentType : DomainEntityId
    {
       
        public Guid PaymentTypeNameId { get; set; }

        [MaxLength(32)]
        //[Display(ResourceType = typeof(Resources.DAL.App.DTO.PaymentType), Name = nameof(PaymentTypeName))]
        public string PaymentTypeName { get; set; } = default!; // Through bank, credit/debit card, paypal
    }
}