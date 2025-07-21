using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.BankProductDto
{
    public class BankDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Bank name")]
        public string BankName { get; set; }

        [Range(1.0, 5.0, ErrorMessage = "{0} must be between {1} and {2}")]
        [Display(Name = "Bank rating")]
        public double Rating { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Has License")]
        public bool HasLicense { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Bank founder full name")]
        public string BankFounderFullName { get; set; }

        //Internal bank information
        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Bank director full name")]
        public string BankDirectorFullName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} cant be lesser than {1} or empty")]
        [Display(Name = "Capitalization")]
        public double Capitalization { get; set; }

        [BindNever]
        public int EmployeesCount { get; set; }

        [BindNever]
        public int BlockedClientsCount { get; set; }

        [BindNever]
        public int ClientsCount { get; set; }

        [BindNever]
        public int ActiveClientsCount { get; set; }

        [StringLength(100)]
        public string? WebsiteUrl { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(20)]
        [Display(Name = "Contact phone")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Established date")]
        public DateTime EstablishedDate { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(100)]
        [Display(Name = "Legal address")]
        public string LegalAddress { get; set; }

        //Not required, if user doesnt add the logo it will contain the default path
        public string? BankLogoPath { get; set; }
    }
}
