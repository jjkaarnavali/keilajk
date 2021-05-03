using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ProductType : DomainEntityId
    {
      
        
        public Guid TypeNameId { get; set; }
        public string TypeName { get; set; } = default!;
    }
}