using Domain.Abstractions;
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
    public class CardTariffsEntity : IHasId
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

        public List<PaymentSystem> EnabledPaymentSystems { get; set; }

        public double? InterestRate { get; set; }

        public List<CardCurrency> EnableCurency { get; set; }

        public int AnnualMaintenanceCost { get; set; }

        public double P2PToAnotherBankCommission { get; set; }

        public double P2PInternalCommission { get; set; }

        [StringLength(4)]
        public string CardNumberMasked { get; set; }

        public ICollection<UserCardEntity> UserCards { get; set; }

        public CardTariffsEntity() { }

        public CardTariffsEntity(Guid bankId, string cardName, CardType type, CardLevel level, double validityPeriod, int maxCreditLimit, 
            List<PaymentSystem> enabledPaymentSystems, double? interestRate, List<CardCurrency> enableCurency, int annualMaintenanceCost, string cardNumberMasked,
            double p2pInternalCommission, double p2pToAnotherBankCommission)
        {
            BankId = bankId;
            CardName = cardName;
            Type = type;
            Level = level;
            ValidityPeriod = validityPeriod;
            MaxCreditLimit = maxCreditLimit;
            EnabledPaymentSystems = enabledPaymentSystems;
            InterestRate = interestRate;
            EnableCurency = enableCurency;
            AnnualMaintenanceCost = annualMaintenanceCost;
            CardNumberMasked = cardNumberMasked;
            UserCards = new List<UserCardEntity>();
            P2PInternalCommission = p2pInternalCommission;
            P2PToAnotherBankCommission = p2pToAnotherBankCommission;
        }
    }
}
