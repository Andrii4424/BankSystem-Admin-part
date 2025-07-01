using Domain.Entities.Persons;
using Domain.Enums.CardEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Banks
{
    public class CardTarrifsEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid BankId { get; private set; }

        [ForeignKey("BankId")]
        public BankEntity Bank { get; private set; }

        [StringLength(40)]
        public string CardName { get; set; }

        public CardType Type { get; set; }

        public CardLevel Level { get; set; }

        public double ValidityPeriod { get; set; }

        public int MaxCreditLimit { get; set; }

        public PaymentSystem[] EnabledPaymentSystem { get; set; }

        public double? InterestRate { get; set; }

        public string[] Curency { get; set; }

        public int AnnualMaintenanceCost { get; set; }

        [StringLength(4)]
        public string CardNumberMasked { get; set; }

        public ICollection<UserCardEntity> UserCards { get; set; }

        public CardTarrifsEntity() { }

        public CardTarrifsEntity(Guid bankId, string cardName, CardType type, CardLevel level, double validityPeriod, int maxCreditLimit, 
            PaymentSystem[] enabledPaymentSystem, double? interestRate, string[] curency, int annualMaintenanceCost, string cardNumberMasked)
        {
            BankId = bankId;
            CardName = cardName;
            Type = type;
            Level = level;
            ValidityPeriod = validityPeriod;
            MaxCreditLimit = maxCreditLimit;
            EnabledPaymentSystem = enabledPaymentSystem;
            InterestRate = interestRate;
            Curency = curency;
            AnnualMaintenanceCost = annualMaintenanceCost;
            CardNumberMasked = cardNumberMasked;
            UserCards = new List<UserCardEntity>();
        }
    }
}
