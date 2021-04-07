using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(128, MinimumLength = 1)]
        public string FirstName { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        public string LastName { get; set; } = default!;
        [StringLength(2, MinimumLength = 1)]
        public string UserLevel { get; set; } = default!;

        public ICollection<Person>? Persons { get; set; }

        

        /*
        public string FullName => FirstName + " " + LastName;
        public string FullNameEmail => FullName + " (" + Email + ")";*/
    }
    
}