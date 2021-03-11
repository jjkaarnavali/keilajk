using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Company : DomainEntityId
    {
      
        
        [MaxLength(64)]
        public string CompanyName { get; set; }  = default!;
        
        [MaxLength(64)]
        public string RegistrationCode { get; set; }  = default!;
        
        public string Phone { get; set; }  = default!;
        
        public string Email { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the companies products added to shop
        
        public DateTime? Until { get; set; } // When the company no longer operates
    }
}