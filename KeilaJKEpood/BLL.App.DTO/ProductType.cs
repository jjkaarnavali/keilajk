using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProductType : DomainEntityId
    {
      
        
        public string TypeName { get; set; }  = default!;
    }
}