﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class PaymentType : DomainEntityId
    {
       

        [MaxLength(32)]
        public string? PaymentTypeName { get; set; } // Through bank, credit/debit card, paypal
    }
}