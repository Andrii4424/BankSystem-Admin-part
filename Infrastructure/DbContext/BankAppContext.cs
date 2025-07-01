using Domain.Entities.Banks;
using Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BankAppContext : DbContext
    {
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<CardTarrifsEntity> CardTarrifs { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<UserCardEntity> UserCards { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
