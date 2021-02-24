using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)]
        public string FirstName { get; set; }  = default!;
        
        [MaxLength(64)]
        public string LastName { get; set; }  = default!;
        
        [MaxLength(11)]
        public string PersonsIdCode { get; set; }  = default!;
         
        
    }
}