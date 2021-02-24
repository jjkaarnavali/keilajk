using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid BillId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public DateTime PaymentTime { get; set; } // When was the bill payed for
    }
}