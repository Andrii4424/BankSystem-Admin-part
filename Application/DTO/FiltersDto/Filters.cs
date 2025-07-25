using Domain.Entities.Banks;
using System.Linq.Expressions;

namespace Application.DTO.FiltersDto
{
    public class Filters<T> where T : class
    {
        public int FirstItem { get; set; }
        public int ElementsToLoad { get; set; }

        public Expression<Func<T, bool>>? SearchFilter { get; set; }

        public Expression<Func<T, object>> SortValue { get; set; }
        public bool Ascending { get; set; }

        public List<Expression<Func<T, bool>>?> EntityFilters { get; set; }

        public Filters(int firstItem, int elementsToLoad, Expression<Func<T, bool>>? searchFilter, Expression<Func<T, object>> sortValue,
            bool ascending, List<Expression<Func<T, bool>>?> entityFilters)
        {
            FirstItem = firstItem;
            ElementsToLoad = elementsToLoad;
            SearchFilter = searchFilter;
            SortValue = sortValue;
            Ascending = ascending;
            EntityFilters = entityFilters;
        }

    }
}
