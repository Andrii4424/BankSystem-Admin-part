using Domain.Entities.Banks;
using Domain.Entities.Persons;
using Domain.Enums.CardEnums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PersonDto
{
    public class UserCardDto
    {
        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Card tariffs Id")]
        public Guid CardTariffsId { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "User Id")]
        public Guid UserId { get; private set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Chosen currency")]
        public string ChosenCurrency { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Pin code")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Chosed payment system")]
        public PaymentSystem ChosedPaymentSystem { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Card status")]
        public CardStatus Status { get; set; }

        [BindNever]
        public int PinTriesLeft { get; set; }

        [BindNever]
        public DateTime? PinUnlockTime { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [StringLength(16)]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Expiration Date")]
        public DateOnly ExpirationDate { get; set; }

        [Required(ErrorMessage = "{0} has to be provided")]
        [Display(Name = "Expiration Date")]
        public string Cvv { get; set; }
    }
}
