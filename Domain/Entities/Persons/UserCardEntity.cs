using Domain.Entities.Banks;
using Domain.Enums.CardEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Persons
{
    public class UserCardEntity
    {
        [Key]
        public Guid Id { get; private set; }

        public Guid CardTariffsId { get; private set; }

        [ForeignKey("CardTarrifsId")]
        public CardTariffsEntity CardTarrifs { get; init; }

        public Guid UserId { get; private set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; init; }

        public double Balance { get; set; }

        public string ChosenCurrency { get; set; }

        public string Pin { get; set; }

        public PaymentSystem ChosedPaymentSystem { get; set; }

        public CardStatus Status { get; set; }

        public int PinTriesLeft { get; set; } = 3;

        public DateTime? PinUnlockTime { get; set; }

        [StringLength(16)]
        public string CardNumber { get; set; }

        public DateOnly ExpirationDate { get; set; }

        public string Cvv { get; set; }

        public UserCardEntity(Guid cardTariffsId, Guid userId, double balance, string chosenCurrency, string pin, 
            PaymentSystem chosedPaymentSystem, DateOnly expirationDate, string cardNumber, string cvv, DateTime? pinUnlockTime, CardStatus status)
        {
            Id = Guid.NewGuid();
            CardTariffsId = cardTariffsId;
            UserId = userId;
            Balance = balance;
            ChosenCurrency = chosenCurrency;
            Pin = pin;
            ChosedPaymentSystem = chosedPaymentSystem;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            Cvv = cvv;
            PinTriesLeft = 3; // default value
            PinUnlockTime = pinUnlockTime;
            Status = status;
        }
    }
}
