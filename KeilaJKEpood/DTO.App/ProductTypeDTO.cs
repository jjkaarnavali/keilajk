using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class ProductTypeDTO
    {   
        public Guid Id { get; set; }
        
        [MaxLength(64)] public string TypeName { get; set; } = default!;
        
        
        
    }
    public class ProductTypeAdd
    {
        [MaxLength(64)] 
        public string TypeName { get; set; } = default!;
    }
}