using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Person : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(64)] public string FirstName { get; set; } = default!;

        [MaxLength(64)] public string LastName { get; set; } = default!;
        
        public string PersonsIdCode { get; set; }  = default!;
        
        
        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        //public string FullName => FirstName + " " + LastName;
       
    }
}