using System;
using Domain.Base;

namespace Domain.App
{
    public class ProductType : DomainEntityId
    {
      
        public Guid TypeNameId { get; set; }
        public LangString? TypeName { get; set; }
    }
}