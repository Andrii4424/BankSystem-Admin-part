using Domain.Abstractions;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class, IHasId
    {
        private readonly BankAppContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BankAppContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public async Task<Boolean> IsObjectIdExists(Guid id)
        {
            return await _dbSet.AnyAsync(obj => obj.Id == id);
        }

        public async Task<IEnumerable<T>?> GetAllValuesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetValueByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateObject(T entity)
        {
            _dbSet.Update(entity);
        }

        public void DeleteElement(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? searchFilter, List<Expression<Func<T, bool>>?> filters)
        {
            var query = _dbSet.AsQueryable();
            if (searchFilter != null) query = query.Where(searchFilter);
            filters = filters.Where(val => val != null).ToList();
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

        public async Task<bool> IsUnique(Expression<Func<T, bool>> searchParametr)
        {
            return await _dbSet.AnyAsync(searchParametr);
        }

        public async Task<List<T>> GetLimitedAsync(int firstElement, int elementsToLoad, Expression<Func<T, bool>>? searchFilter, bool ascending, 
            Expression<Func<T, object>> sortValue, List<Expression<Func<T, bool>>?> filters)
        {
            var query = _dbSet.AsQueryable();
            if(searchFilter!=null) query = query.Where(searchFilter);
            filters = filters.Where(v => v != null).ToList();
            foreach(var filter in filters)
            {
                query = query.Where(filter);
            }
            query= ascending? query.OrderBy(sortValue): query.OrderByDescending(sortValue);

            return await query
                .Skip(firstElement)
                .Take(elementsToLoad)
                .ToListAsync();
        }
    }
}
