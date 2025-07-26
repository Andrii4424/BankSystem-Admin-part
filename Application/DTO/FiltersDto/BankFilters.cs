using Domain.Entities.Banks;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class BankFilters
    {
        public int FirstElement { get; set; }
        public int ElementsToLoad { get; set; }

        //Filters, search and sort settings
        public string? SearchValue { get; set; }

        public string SortValue { get; set; }
        public bool ascending {  get; set; }

        public bool? licenseFilter { get; set; } 
        public bool? siteFilter { get; set; } 
        public double? ratingFilter { get; set; }
        public int? clientsCountFilter { get; set; }
        public int? capitalizationFilter { get; set; }

        public BankFilters()
        {
            FirstElement = 0;
            ElementsToLoad = 6;
        }

        public Filters<BankEntity> ToGeneralFilters()
        {
            Expression<Func<BankEntity, bool>>? searchFilter = SearchValue != null ? c => c.BankName.Contains(SearchValue.Trim()) : null;
            Expression<Func<BankEntity, object>>? sortExpression;

            ascending = true;
            switch (SortValue)
            {
                case "oldest":
                    sortExpression = b => b.EstablishedDate;
                    break;
                case "newest":
                    sortExpression = b => b.EstablishedDate;
                    ascending = false;
                    break;
                case "rating-descending":
                    sortExpression = b => b.Rating;
                    ascending = false;
                    break;
                case "popularity-descending":
                    sortExpression = b => b.ActiveClientsCount;
                    ascending = false;
                    break;
                default:
                    sortExpression = b => b.EstablishedDate;
                    break;
            }

            List<Expression<Func<BankEntity, bool>>?> filters = new();

            filters.Add((licenseFilter.HasValue && licenseFilter == true) ? b => b.HasLicense : null);
            filters.Add((siteFilter.HasValue && siteFilter == true) ? b => b.WebsiteUrl != null : null);
            filters.Add((ratingFilter.HasValue && ratingFilter != 0) ? b => b.Rating >= ratingFilter : null);
            filters.Add((clientsCountFilter.HasValue && clientsCountFilter != 0) ? b => b.ActiveClientsCount >= clientsCountFilter : null);
            filters.Add((capitalizationFilter.HasValue && capitalizationFilter != 0) ? b => b.Capitalization >= capitalizationFilter : null);

            return new Filters<BankEntity>(FirstElement, ElementsToLoad, searchFilter, sortExpression, ascending, filters);
        }

    }
}
