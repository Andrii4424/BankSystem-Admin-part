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

        public async Task<List<BankEntity>?> GetLimitedBankList<TSel>(int firstItem, int countOfItems,
            Expression<Func<BankEntity, TSel>> selector, bool ascending=true)
        {
            var _dbSetSorted = ascending? _dbSet.OrderBy(selector): _dbSet.OrderByDescending(selector);
            return await _dbSetSorted
             .ThenBy(b=>b.Id)
            .Skip(firstItem)
            .Take(countOfItems)
            .ToListAsync();
        }
    }
}
