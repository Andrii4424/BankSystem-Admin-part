using Domain.Abstractions;
using Domain.Entities.Banks.BankProducts;
using Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Banks
{
    public class BankEntity : IHasId
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [StringLength(40)]
        public string BankName { get; set; }

        public double Rating { get; set; }

        public bool HasLicense { get; set; }

        [StringLength(40)]
        public string BankFounderFullName { get; set; }

        //Internal bank information
        [StringLength(40)]
        public string BankDirectorFullName { get; set; }

        public double Capitalization { get; set; }

        public int EmployeesCount { get; set; }

        public int BlockedClientsCount { get; set; }

        public int ClientsCount { get; set; }

        public int ActiveClientsCount { get; set; }

        [StringLength(100)]
        public string? WebsiteUrl { get; set; }

        [StringLength(20)]
        public string ContactPhone { get; set; }

        public DateTime EstablishedDate { get; set; }

        [StringLength(100)]
        public string LegalAddress { get; set; }

        public string BankLogoPath { get; set; }

        //Child elements
        public ICollection<CardTariffsEntity> Cards { get; set; }

        public ICollection<UserEntity> Users { get; set; }

        public ICollection<CreditTariffsEntity> Credits { get; set; }

        public ICollection<DepositTariffsEntity> Deposits { get; set; }

        public BankEntity()
        {
            Users = new List<UserEntity>();
            Cards = new List<CardTariffsEntity>();
        }

        public BankEntity(string bankName, double rating, bool hasLicense, string bankFounderFullName, string bankDirectorFullName,
            double capitalization, string? websiteUrl, string contactPhone, DateTime establishedDate, string legalAddress)
        {
            BankName = bankName;
            Rating = rating;
            HasLicense = hasLicense;
            BankFounderFullName = bankFounderFullName;
            BankDirectorFullName = bankDirectorFullName;
            Capitalization = capitalization;
            WebsiteUrl = websiteUrl;
            ContactPhone = contactPhone;
            EstablishedDate = establishedDate;
            LegalAddress = legalAddress;
            Users = new List<UserEntity>();
            Cards = new List<CardTariffsEntity>();
            Credits = new List<CreditTariffsEntity>();
            Deposits = new List<DepositTariffsEntity>();
        }
    }
}
