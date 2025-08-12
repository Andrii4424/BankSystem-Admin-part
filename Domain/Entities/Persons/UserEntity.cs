using Domain.Abstractions;
using Domain.Entities.Banks;
using Domain.Enums.UserEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Persons
{
    public class UserEntity :IHasId
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid BankId { get; private set; }

        [ForeignKey("BankId")]
        public BankEntity Bank { get; private set; }

        [StringLength(20)]
        public string FinancalNumber { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        [StringLength(40)]
        public string Surname { get; set; }

        [StringLength(40)]
        public string Patronymic { get; set; }

        [StringLength(40)]
        public string TaxId {  get; set; }

        [StringLength(40)]
        public string PassportNumber { get; set; }

        [StringLength(4)]
        public string BankPassword { get; set; }

        public string? ProfilePicturePath { get; set; }

        public DateTime Birthday { get; set; }

        public Nationality UserNationality { get; set; }

        public Gender UserGender { get; set; }

        public ICollection<UserCardEntity> UserCards { get; set; }
        public ICollection<UserPhotosEntity> UserPhotos { get; set; }

        public bool IsEmployeed { get; set; }

        public EmployeeEntity employee { get; set; }

        public UserEntity() { }

        public UserEntity(Guid bankId, string financalNumber, string email, string firstName, string surname, string patronymic,
            string taxId, string passportNumber, string bankPassword, string? profilePicturePath, DateTime birthday, 
            Nationality userNationality, Gender userGender, bool isEmployeed)
        {
            BankId = bankId;
            FinancalNumber = financalNumber;
            Email = email;
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            TaxId = taxId;
            PassportNumber = passportNumber;
            BankPassword = bankPassword;
            ProfilePicturePath = profilePicturePath;
            Birthday = birthday;
            UserNationality = userNationality;
            UserGender = userGender;
            UserCards = new List<UserCardEntity>();
            UserPhotos = new List<UserPhotosEntity>();
            IsEmployeed = isEmployeed;
        }
    }
}
