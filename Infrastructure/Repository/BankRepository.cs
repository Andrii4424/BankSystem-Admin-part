using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<BankEntity>?> GetLimitedBankList(int firstItem, int countOfItems)
        {
            return await _dbSet
                .OrderByDescending(x => x.EstablishedDate)
                .Skip(firstItem)
                .Take(countOfItems)
                .ToListAsync();
        }
    }
}
