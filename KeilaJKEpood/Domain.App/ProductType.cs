using System;
using Domain.Base;

namespace Domain.App
{
    public class ProductType : DomainEntityId
    {
      
        
        public string TypeName { get; set; }  = default!;
    }
}