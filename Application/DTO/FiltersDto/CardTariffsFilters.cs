using Domain.Entities.Banks;
using Domain.Enums.CardEnums;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class CardTariffsFilters
    {
        public int FirstElement { get; set; }
        public int ElementsToLoad { get; set; }

        //Filters, search and sort settings
        public string? SearchValue { get; set; }

        public string? SortValue { get; set; }

        public string? ChosenBankName { get; set; }
        public PaymentSystem? ChosenPaymentSystem { get; set; }
        public List<CardCurrency>? ChosenCurrency { get; set; }
        public CardLevel? ChosenLevel { get; set; }
        public CardType? ChosenType { get; set; }

        public Filters<CardTariffsEntity> ToGeneralFilters()
        {
            Expression<Func<CardTariffsEntity, bool>>? searchFilter = SearchValue != null ? c => c.CardName.Contains(SearchValue.Trim()) : null;

            Expression<Func<CardTariffsEntity, object>>? sortExpression;
            bool ascending;

            switch (SortValue) {
                case "name-descending":
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
                case "name-ascending":
                    ascending = true;
                    sortExpression = c => c.CardName;
                    break;
                case "annual-maintenance-cost":
                    ascending = false;
                    sortExpression = c => c.AnnualMaintenanceCost;
                    break;
                case "validity-period":
                    ascending = true;
                    sortExpression = c => c.ValidityPeriod;
                    break;
                default:
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
            }

            List<Expression<Func<CardTariffsEntity, bool>>?> filters = new();

            if (ChosenBankName != null) filters.Add(c => c.Bank.BankName == ChosenBankName);
            if (ChosenPaymentSystem != null) filters.Add(c => c.EnabledPaymentSystems.Contains(ChosenPaymentSystem.Value));
            if (ChosenLevel != null) filters.Add(c => c.Level == ChosenLevel);
            if(ChosenType != null) filters.Add(c=>  c.Type == ChosenType);
            if (ChosenCurrency != null)
            {
                foreach(var item in ChosenCurrency)
                {
                    filters.Add(c => c.EnableCurency.Contains(item));
                }
            }
            return new Filters<CardTariffsEntity>(FirstElement, ElementsToLoad, searchFilter, sortExpression, ascending, filters);
        }
    }
}
