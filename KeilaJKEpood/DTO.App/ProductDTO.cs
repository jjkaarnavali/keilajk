using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class ProductDTO
    {   
        public Guid Id { get; set; }
        
        [MaxLength(64)] public Guid CompanyId { get; set; } = default!;

        [MaxLength(64)] public Guid ProductTypeId { get; set; } = default!;
        
        public string ProductName { get; set; }  = default!;
        public string ProductSize { get; set; }  = default!;
        public string ProductSeason { get; set; }  = default!;
        public string ProductCode { get; set; }  = default!;
        public DateTime From { get; set; }  = default!;
        public DateTime Until { get; set; }  = default!;
        
        
        
    }
    public class ProductAdd
    {
        [MaxLength(64)] public Guid CompanyId { get; set; } = default!;
        [MaxLength(64)] public Guid ProductTypeId { get; set; } = default!;
        public string ProductName { get; set; }  = default!;
        public string ProductSize { get; set; }  = default!;
        public string ProductSeason { get; set; }  = default!;
        public string ProductCode { get; set; }  = default!;
    }
}