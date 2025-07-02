using Domain.Entities.Banks;
using Domain.Enums.CardEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.BankDto
{
    public class CardTariffsDto :IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Bank Id")]
        public Guid BankId { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(40)]
        [Display(Name = "Card name")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Card type")]
        public CardType Type { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Card level")]
        public CardLevel Level { get; set; }

        [Range(1, 99, ErrorMessage = "{0} must be between {1} and {2}")]
        [Display(Name = "Validity period")]
        public double ValidityPeriod { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} cant be lesser than 0 or empty")]
        [Display(Name = "Max credit limit")]
        public int MaxCreditLimit { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Payment system")]
        public PaymentSystem[] EnabledPaymentSystem { get; set; }

        public double? InterestRate { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Curency")]
        public string[] Curency { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Annual maintenance cost")]
        public int AnnualMaintenanceCost { get; set; }

        [Range(0, 25.00, ErrorMessage = "{0} must be between {1} and {2}")]
        [Display(Name = "Max credit limit")]
        public double P2PToAnotherBankCommission { get; set; }

        [Range(0, 10.00, ErrorMessage = "{0} must be between {1} and {2}")]
        [Display(Name = "Max credit limit")]
        public double P2PInternalCommission { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(4)]
        [Display(Name = "Card number masked")]
        public string CardNumberMasked { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (P2PInternalCommission > P2PToAnotherBankCommission) yield return new ValidationResult("P2P commision to " +
                "another bank cant be lesser than internal P2P commision"); 
            if(Type == CardType.Debit && MaxCreditLimit>0) yield return new ValidationResult("Debit card cant have credit limit");
            if(Type == CardType.Debit && InterestRate!=null) yield return new ValidationResult("Debit card cant have interest rate");
            if (ValidityPeriod % 0.5 != 0) yield return new ValidationResult("The card validity period must be a multiple of 1 year " +
                "or half a year (0.5)");
            throw new NotImplementedException();
        }
    }
}
