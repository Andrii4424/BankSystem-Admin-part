using Domain.Entities.Banks;
using Domain.Entities.Persons;
using Domain.Enums.CardEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Newtonsoft.Json;
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
        public DbSet<CardTariffsEntity> CardTariffs { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<UserCardEntity> UserCards { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankEntity>().ToTable("Banks");
            modelBuilder.Entity<CardTariffsEntity>().ToTable("CardTariffs");
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");
            modelBuilder.Entity<UserCardEntity>().ToTable("UserCards");
            modelBuilder.Entity<UserEntity>().ToTable("Users");

            //Enum converter
            modelBuilder.Entity<CardTariffsEntity>(c => {
                c.Property(card => card.Type).HasConversion<string>();
                c.Property(card => card.Level).HasConversion<string>();
                c.Property(card => card.EnabledPaymentSystem)
                 .HasConversion(
                     v => JsonConvert.SerializeObject(v),
                     v => JsonConvert.DeserializeObject<PaymentSystem[]>(v)
                 );
            });

            modelBuilder
                .Entity<EmployeeEntity>()
                .Property(c => c.JobTitle)
                .HasConversion<string>();

            modelBuilder.Entity<UserEntity>(u =>
            {
                u.Property(user => user.UserNationality).HasConversion<string>();
                u.Property(user => user.UserGender).HasConversion<string>();
            });


            modelBuilder.Entity<UserCardEntity>(c =>
            {
                c.Property(card => card.ChosedPaymentSystem).HasConversion<string>();
                c.Property(card => card.Status).HasConversion<string>();
            });
        }

        public BankAppContext(DbContextOptions<BankAppContext> options) :base(options)
        {

        }
    }
}
