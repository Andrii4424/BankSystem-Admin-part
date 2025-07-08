using Domain.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryContracts
{
    public interface IBankRepository :IGenericRepository<BankEntity>
    {
        public Task<List<BankEntity>?> GetLimitedBankList(int firstItem, int countOfItems);
    }
}
