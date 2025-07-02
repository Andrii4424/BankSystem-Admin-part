using Domain.Entities.Persons;
using Domain.Enums.EmployeeEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PersonDto
{
    public class EmployeeDto
    {
        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "User Id")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Job title")]
        public JobTitles JobTitle { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Salary")]
        public int Salary { get; set; } //in hryvnia

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
    }
}
