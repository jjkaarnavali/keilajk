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
        public Guid FirstNameId { get; set; }
        [MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        [MaxLength(64, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Person), Name = "FirstName")]
        public string FirstName { get; set; } = default!;

        public Guid LastNameId { get; set; }
        [MaxLength(64)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Person), Name = "LastName")]
        public string LastName { get; set; } = default!;
        
        public Guid PersonsIdCodeId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Person), Name = "PersonsIdCode")]
        public string PersonsIdCode { get; set; }  = default!;
        
        
        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        //public string FullName => FirstName + " " + LastName;
       
    }
}