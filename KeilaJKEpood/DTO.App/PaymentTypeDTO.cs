using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class PaymentTypeDTO
    {   
        public Guid Id { get; set; }
        
        [MaxLength(64)] public string PaymentTypeName { get; set; } = default!;
        
        
        
    }
    public class PaymentTypeAdd
    {
        [MaxLength(64)] 
        public string PaymentTypeName { get; set; } = default!;
    }
}