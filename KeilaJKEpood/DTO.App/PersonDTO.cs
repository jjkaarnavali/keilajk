using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class PersonDTO
    {   
        public Guid Id { get; set; }
        
        [MaxLength(64)] public string FirstName { get; set; } = default!;

        [MaxLength(64)] public string LastName { get; set; } = default!;
        
        public string PersonsIdCode { get; set; }  = default!;
        
        

    }
    public class PersonAdd
    {
        [MaxLength(64)] 
        public string FirstName { get; set; } = default!;

        [MaxLength(64)] 
        public string LastName { get; set; } = default!;
        
        public string PersonsIdCode { get; set; }  = default!;
    }

    
}