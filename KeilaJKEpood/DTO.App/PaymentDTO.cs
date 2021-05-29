using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class PaymentDTO
    {
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