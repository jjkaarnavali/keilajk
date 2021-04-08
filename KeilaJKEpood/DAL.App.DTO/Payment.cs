using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Payment : DomainEntityId
    {
    
        
        public Guid PaymentTypeId { get; set; }

        public Guid BillId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public DateTime PaymentTime { get; set; } // When was the bill payed for
    }
}