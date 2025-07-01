using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Banks.BankProducts
{
    public class CreditTariffsEntity
    {
        [Key]
        public Guid Id { get; private set; }

        public Guid BankId { get; private set; }

        [ForeignKey("BankId")]
        public BankEntity Bank { get; private set; }

        public string CreditName { get; set; }

        public double MonthlyInterestRate { get; set; }

        public int[] AvailableCreditPeriods { get; set; }

        public double MaxCreditAmount { get; set; }

        public double MinCreditAmount { get; set; }

        public CreditTariffsEntity(Guid bankId, string creditName, double monthlyInterestRate, int[] availableCreditPeriods, 
            double maxCreditAmount, double minCreditAmount)
        {
            BankId = bankId;
            CreditName = creditName;
            MonthlyInterestRate = monthlyInterestRate;
            AvailableCreditPeriods = availableCreditPeriods;
            MaxCreditAmount = maxCreditAmount;
            MinCreditAmount = minCreditAmount;
        }
    }
}
