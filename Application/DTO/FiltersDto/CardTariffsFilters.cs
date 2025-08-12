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
        public List<PaymentSystem>? ChosenPaymentSystems { get; set; }
        public List<CardCurrency>? ChosenCurrency { get; set; }
        public List<CardLevel>? ChosenLevels { get; set; }
        public List<CardType>? ChosenTypes { get; set; }

        public CardTariffsFilters()
        {
            FirstElement = 0;
            ElementsToLoad = 12;
        }

        public Filters<CardTariffsEntity> ToGeneralFilters()
        {
            Expression<Func<CardTariffsEntity, bool>>? searchFilter = SearchValue != null ? c => c.CardName.Contains(SearchValue.Trim()) : null;

            Expression<Func<CardTariffsEntity, object>>? sortExpression;
            bool ascending;

            switch (SortValue) {
                case "name-descending":
                    ascending = true;
                    sortExpression = c => c.CardName;
                    break;
                case "name-ascending":
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
                case "annual-maintenance-cost":
                    ascending = true;
                    sortExpression = c => c.AnnualMaintenanceCost;
                    break;
                case "validity-period":
                    ascending = false;
                    sortExpression = c => c.ValidityPeriod;
                    break;
                default:
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
            }

            List<Expression<Func<CardTariffsEntity, bool>>?> filters = new();

            if (ChosenBankName != null) filters.Add(c => c.Bank.BankName.Contains(ChosenBankName));
            if (ChosenPaymentSystems != null)
            {
                filters.Add(c => c.EnabledPaymentSystems.Any(paymentsystem => ChosenPaymentSystems.Contains(paymentsystem)));
            }
            if (ChosenCurrency != null)  // Попробуй через элемент коллекции [0]
            {
                filters.Add(c => c.EnableCurency.Any(currency => ChosenCurrency.Contains(currency)));
            }
            if (ChosenLevels != null)
            {
                foreach (var item in ChosenLevels)
                {
                    filters.Add(c => ChosenLevels.Contains(c.Level));
                }
            }
            if (ChosenTypes != null)
            {
                foreach (var item in ChosenTypes)
                {
                    filters.Add(c => ChosenTypes.Contains(c.Type));
                }
            }
            return new Filters<CardTariffsEntity>(FirstElement, ElementsToLoad, searchFilter, sortExpression, ascending, filters);
        }
    }
}
