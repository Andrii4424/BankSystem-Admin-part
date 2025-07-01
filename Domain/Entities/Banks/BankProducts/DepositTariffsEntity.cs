using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Banks.BankProducts
{
    public class DepositTariffsEntity
    {
        [Key]
        public Guid Id { get; private set; }

        public Guid BankId { get; private set; }

        [ForeignKey("BankId")]
        public BankEntity Bank { get; private set; }

        public string DepositName { get; set; }

        public double AnnualInterestRate { get; set; }

        public int[] AvailableDepositPeriods { get; set; }

        public double MinDepositAmount { get; set; }

        public DepositTariffsEntity(Guid bankId, string depositName, double annualInterestRate, int[] availableDepositPeriods, double minDepositAmount)
        {
            BankId = bankId;
            DepositName = depositName;
            AnnualInterestRate = annualInterestRate;
            AvailableDepositPeriods = availableDepositPeriods;
            MinDepositAmount = minDepositAmount;
        }
    }
}
