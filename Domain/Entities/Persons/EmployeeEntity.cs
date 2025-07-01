using Domain.Enums.EmployeeEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Persons
{
    public class EmployeeEntity
    {
        [Key]
        public Guid Id {  get; private set; }

        public Guid UserId { get; private set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; private set; }

        public JobTitles JobTitle { get; set; }
        
        public int Salary { get; set; } //in hryvnia

        public DateTime HireDate { get; set; }

        public EmployeeEntity(Guid userId, JobTitles jobTitle, int salary, DateTime hireDate)
        {
            UserId = userId;
            JobTitle = jobTitle;
            Salary = salary;
            HireDate = hireDate;
        }
    }
}
