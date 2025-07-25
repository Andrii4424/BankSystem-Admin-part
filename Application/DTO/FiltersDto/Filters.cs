using Domain.Entities.Banks;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class Filters<T> where T : class
    {
        public int FirstItem { get; set; }
        public int ElementsToLoad { get; set; }

        Expression<Func<T, bool>>? SearchFilter { get; set; }

        Expression<Func<T, object>> Selector { get; set; }
        bool Ascending { get; set; }

        List<Expression<Func<CardTariffsEntity, bool>>?> EntityFilters { get; set; }

        public Filters(int firstItem, int elementsToLoad, Expression<Func<T, bool>>? searchFilter, Expression<Func<T, object>> selector,
            bool ascending, List<Expression<Func<CardTariffsEntity, bool>>?> entityFilters)
        {
            FirstItem = firstItem;
            ElementsToLoad = elementsToLoad;
            SearchFilter = searchFilter;
            Selector = selector;
            Ascending = ascending;
            EntityFilters = entityFilters;
        }

    }
}
