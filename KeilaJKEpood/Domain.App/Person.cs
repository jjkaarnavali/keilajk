using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

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
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        
        //public string IdentityUserId { get; set; } = default!;
        //public IdentityUser? IdentityUser { get; set; }


    }
}