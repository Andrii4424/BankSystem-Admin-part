using Domain.Entities.Persons;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository:GenericRepository<UserEntity>, IUserRepository
    {
        private readonly DbSet<UserEntity> _dbSet;

        public UserRepository(BankAppContext dbContext): base(dbContext)
        {
            _dbSet = dbContext.Set<UserEntity>();
        }
    }
}
