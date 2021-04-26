using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Person : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(64)]
        [Display(ResourceType = typeof(Resources.DAL.App.DTO.Person), Name = "FirstName")]
        public string FirstName { get; set; } = default!;

        [MaxLength(64)] 
        [Display(ResourceType = typeof(Resources.DAL.App.DTO.Person), Name = "LastName")]
        public string LastName { get; set; } = default!;
        
        [Display(ResourceType = typeof(Resources.DAL.App.DTO.Person), Name = "PersonsIdCode")]
        public string PersonsIdCode { get; set; }  = default!;
        
        
        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        //public string FullName => FirstName + " " + LastName;
       
    }
}