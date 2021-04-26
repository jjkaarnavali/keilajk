using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductType : DomainEntityId
    {
      
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ProductType), Name = "TypeName")]
        public string TypeName { get; set; }  = default!;
    }
}