using Domain.Entities.Banks;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CardTarrifsRepository :GenericRepository<CardTariffsEntity>, ICardTarrifsRepository
    {
        private readonly DbSet<CardTariffsEntity> _dbSet;
        public CardTarrifsRepository(BankAppContext dbContext): base(dbContext) 
        {
            _dbSet = dbContext.Set<CardTariffsEntity>();
        }
    }
}
