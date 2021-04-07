using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using DAL.App.DTO;
namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(128, MinimumLength = 1)]
        public string Firstname { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        public string Lastname { get; set; } = default!;
        
        [StringLength(2, MinimumLength = 1)]
        public string UserLevel { get; set; } = default!;

        public ICollection<Person>? Persons { get; set; }
        
        //public ICollection<Simple>? Simples { get; set; }

       /* public string FullName => Firstname + " " + Lastname;
        public string FullNameEmail => FullName + " (" + Email + ")";*/
    }
    
}