using Domain.Entities.Banks;
using Domain.Entities.Persons;
using Domain.Enums.UserEnums;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class UserFilters
    {
        public int? FirstElement { get; set; }
        public int? ElementsToLoad { get; set; }

        //Filters, search and sort settings
        public string? SearchValue { get; set; }

        public string SortValue { get; set; }
        public bool ascending { get; set; }

        public string? ChosenBankName { get; set; }
        public bool? Employeed { get; set; }
        public Gender? Gender { get; set; }
        public Nationality? UserNationality { get; set; }
        public bool? WithProfilePicture { get; set; }

        public UserFilters(){
            FirstElement= 0; 
            ElementsToLoad = 12;
        }
        
        public Filters<CardTariffsEntity> ToGeneralFilters()
        {
            Expression<Func<CardTariffsEntity, bool>>? searchFilter = SearchValue != null ? c => c.CardName.Contains(SearchValue.Trim()) : null;

            Expression<Func<CardTariffsEntity, object>>? sortExpression;
            bool ascending;

            switch (SortValue)
            {
                case "name-descending":
                    ascending = true;
                    sortExpression = c => c.CardName;
                    break;
                case "name-ascending":
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
                case "age-ascending":
                    ascending = true;
                    sortExpression = c => c.AnnualMaintenanceCost;
                    break;
                case "age-descending":
                    ascending = false;
                    sortExpression = c => c.ValidityPeriod;
                    break;
                default:
                    ascending = false;
                    sortExpression = c => c.CardName;
                    break;
            }

            List<Expression<Func<UserEntity, bool>>?> filters = new();

            if (ChosenBankName != null) filters.Add(u => u.Bank.BankName.Contains(ChosenBankName));
            if (Employeed != null)
            {
                filters.Add(u => u.IsEmployeed==true);
            }
            if (Gender != null)  // Попробуй через элемент коллекции [0]
            {
                filters.Add(u=> u.UserGender==Gender);
            }
            if (UserNationality != null)
            {
                filters.Add(u => u.UserNationality == this.UserNationality);
            }
            if (WithProfilePicture != null)
            {
                filters.Add(u => u.UserNationality != "~");
            }
            return new Filters<CardTariffsEntity>(FirstElement, ElementsToLoad, searchFilter, sortExpression, ascending, filters);
        }
    }
}
