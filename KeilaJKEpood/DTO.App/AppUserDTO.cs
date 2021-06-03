using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO;
using Microsoft.AspNetCore.Identity;

namespace DTO.App
{
    public class AppUserDTO : IdentityUser<Guid>
    {
        [StringLength(128, MinimumLength = 1)]
        public string Firstname { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        public string Lastname { get; set; } = default!;
        
        [StringLength(2, MinimumLength = 1)]
        public string UserLevel { get; set; } = default!;

        public ICollection<Person>? Persons { get; set; }
    }
}