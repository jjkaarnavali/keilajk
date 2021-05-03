using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Person : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        
        public Guid FirstNameId { get; set; }
        [MaxLength(64)]
        public LangString? FirstName { get; set; }
        public Guid LastNameId { get; set; }
        [MaxLength(64)]
        public LangString? LastName { get; set; }
        public Guid PersonsIdCodeId { get; set; }
        [MaxLength(11)]
        public LangString? PersonsIdCode { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        
        //public string IdentityUserId { get; set; } = default!;
        //public IdentityUser? IdentityUser { get; set; }


    }
}