using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PaymentType
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string PaymentTypeName { get; set; } = default!; // Through bank, credit/debit card, paypal
    }
}