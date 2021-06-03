using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.App
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public string CompanyName { get; set; }  = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Company), Name = "RegistrationCode")]
        [MaxLength(64)]
        public string RegistrationCode { get; set; }  = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Company), Name = "Phone")]
        public string Phone { get; set; }  = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Company), Name = "Email")]
        public string Email { get; set; }  = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Company), Name = "From")]
        public DateTime From { get; set; } // When was the companies products added to shop
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Company), Name = "Until")]
        public DateTime? Until { get; set; } // When the company no longer operates
    }
}