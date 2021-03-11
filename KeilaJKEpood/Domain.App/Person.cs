using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Person : DomainEntityId
    {
        
        
        [MaxLength(64)]
        public string FirstName { get; set; }  = default!;
        
        [MaxLength(64)]
        public string LastName { get; set; }  = default!;
        
        [MaxLength(11)]
        public string PersonsIdCode { get; set; }  = default!;
         
        
    }
}