using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class PaymentType : DomainEntityId
    {
       

        [MaxLength(32)]
        public string PaymentTypeName { get; set; } = default!; // Through bank, credit/debit card, paypal
    }
}