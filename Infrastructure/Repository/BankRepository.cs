using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BankRepository: GenericRepository<BankEntity>, IBankRepository
    {
        public BankRepository(BankAppContext dbContext) : base(dbContext) { }
    }
}
