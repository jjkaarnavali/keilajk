using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ProductType : DomainEntityId
    {
      
        
        public string TypeName { get; set; }  = default!;
    }
}