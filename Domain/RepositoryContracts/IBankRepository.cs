using Domain.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryContracts
{
    public interface IBankRepository :IGenericRepository<BankEntity>
    {
        public Task<List<BankEntity>?> GetLimitedBankList<TSelector>(int firstItem, int countOfItems,
            Expression<Func<BankEntity, TSelector>> selector, bool ascending, List<Expression<Func<BankEntity, bool>>?> filters);
    }
}
