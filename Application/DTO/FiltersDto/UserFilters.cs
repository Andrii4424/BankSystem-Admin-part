using Domain.Entities.Banks;
using Domain.Entities.Persons;
using Domain.Enums.UserEnums;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class UserFilters
    {
        public int FirstElement { get; set; }
        public int ElementsToLoad { get; set; }

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
        
        public Filters<UserEntity> ToGeneralFilters()
        {
            Expression<Func<UserEntity, bool>>? searchFilter = SearchValue != null ? u => (u.FirstName.Contains(SearchValue.Trim()) 
            || u.Surname.Contains(SearchValue.Trim()) || u.Patronymic.Contains(SearchValue.Trim())) : null;

            Expression<Func<UserEntity, object>>? sortExpression;
            bool ascending;

            switch (SortValue)
            {
                case "name-descending":
                    ascending = true;
                    sortExpression = u => u.Surname;
                    break;
                case "name-ascending":
                    ascending = false;
                    sortExpression = u => u.Surname;
                    break;
                case "age-ascending":
                    ascending = true;
                    sortExpression = u => u.Birthday;
                    break;
                case "age-descending":
                    ascending = false;
                    sortExpression = u => u.Birthday;
                    break;
                default:
                    ascending = false;
                    sortExpression = u => u.Surname;
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
                filters.Add(u => u.ProfilePicturePath != "uploads/no-avatar.svg");
            }
            return new Filters<UserEntity>(FirstElement, ElementsToLoad, searchFilter, sortExpression, ascending, filters);
        }
    }
}
