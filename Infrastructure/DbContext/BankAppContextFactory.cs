using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext
{
    public class BankAppContextFactory : IDesignTimeDbContextFactory<BankAppContext>
    {
        public BankAppContext CreateDbContext(string[] args) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankAppContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BankSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new BankAppContext(optionsBuilder.Options);
        }
    }
}
