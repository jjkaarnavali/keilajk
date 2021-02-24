using System;

namespace Domain
{
    public class ProductType
    {
        public Guid Id { get; set; }
        
        public string TypeName { get; set; }  = default!;
    }
}