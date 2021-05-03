using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Company : DomainEntityId
    {
      
        public Guid CompanyNameId { get; set; }
        [MaxLength(64)]
        public LangString? CompanyName { get; set; }
        
        public Guid RegistrationCodeId { get; set; }
        [MaxLength(64)]
        public LangString? RegistrationCode { get; set; }
        
        public Guid PhoneId { get; set; }
        public LangString? Phone { get; set; }
        
        public Guid EmailId { get; set; }
        public LangString? Email { get; set; }
        
        public DateTime From { get; set; } // When was the companies products added to shop
        
        public DateTime? Until { get; set; } // When the company no longer operates
    }
}