using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Company : DomainEntityId
    {
      
        public Guid CompanyNameId { get; set; }
        [MaxLength(64)]
        public string CompanyName { get; set; }  = default!;
        
        public Guid RegistrationCodeId { get; set; }
        [MaxLength(64)]
        public string RegistrationCode { get; set; }  = default!;
        
        public Guid PhoneId { get; set; }
        public string Phone { get; set; }  = default!;
        
        public Guid EmailId { get; set; }
        public string Email { get; set; }  = default!;
        
        public DateTime From { get; set; } // When was the companies products added to shop
        
        public DateTime? Until { get; set; } // When the company no longer operates
    }
}