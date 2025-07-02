using Domain.Entities.Banks;
using Domain.Enums.UserEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PersonDto
{
    public class UserDto
    {
        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Bank Id")]
        public Guid BankId { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Phone(ErrorMessage ="{0} must be in phone format")]
        [StringLength(20)]
        [Display(Name = "Financal number")]
        public string FinancalNumber { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Tax id")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Passport number")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(4)]
        [Display(Name = "Bank password")]
        public string BankPassword { get; set; }

        public string? ProfilePicturePath { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Document photo path")]
        public string DocumentPhotoPath { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Nationality")]
        public Nationality UserNationality { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Gender")]
        public Gender UserGender { get; set; }
    }
}
