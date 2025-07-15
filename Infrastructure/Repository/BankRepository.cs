using Domain.Entities.Banks;
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
    public class BankRepository: GenericRepository<BankEntity>, IBankRepository
    {
        private readonly DbSet<BankEntity> _dbSet;
        public BankRepository(BankAppContext dbContext) : base(dbContext) {
            _dbSet = dbContext.Set<BankEntity>();
        }

        public async Task<List<BankEntity>?> GetLimitedBankList<TSelector>(int firstItem, int countOfItems, 
            Expression<Func<BankEntity, bool>>? searchFilter, Expression<Func<BankEntity, TSelector>> selector, 
            bool ascending, List<Expression<Func<BankEntity, bool>>?> filters)
        {
            var query = _dbSet.AsQueryable();
            if(searchFilter!=null) query = query.Where(searchFilter);
            filters = filters.Where(val => val!= null).ToList();
            foreach (var filter in filters) {
                query = query.Where(filter);
            }
            query = (ascending? query.OrderBy(selector): query.OrderByDescending(selector)).ThenBy(b => b.Id);
            return await query
            .Skip(firstItem)
            .Take(countOfItems)
            .ToListAsync();
        }

    }
}
