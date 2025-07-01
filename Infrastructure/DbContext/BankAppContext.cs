using Domain.Entities.Banks;
using Domain.Entities.Banks.BankProducts;
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
    public class BankAppContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BankAppContext(DbContextOptions<BankAppContext> options) : base(options)
        {

        }

        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<CardTariffsEntity> CardTariffs { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<UserCardEntity> UserCards { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CreditTariffsEntity> CreditTariffs { get; set; }
        public DbSet<DepositTariffsEntity> DepositTariffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankEntity>().ToTable("Banks");
            modelBuilder.Entity<CardTariffsEntity>().ToTable("CardTariffs");
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");
            modelBuilder.Entity<UserCardEntity>().ToTable("UserCards");
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<CreditTariffsEntity>().ToTable("CreditTariffs");
            modelBuilder.Entity<DepositTariffsEntity>().ToTable("DepositTariffs");

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

            //Relationship
            modelBuilder.Entity<CardTariffsEntity>()
                .HasOne(c => c.Bank)
                .WithMany(b=>b.Cards)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Bank)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(e => e.User)
                .WithOne(u => u.employee)
                .HasForeignKey<EmployeeEntity>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCardEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.UserCards)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCardEntity>()
                .HasOne(c => c.CardTarrifs)
                .WithMany(ct => ct.UserCards)
                .HasForeignKey(u => u.CardTariffsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditTariffsEntity>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Credits)
                .HasForeignKey(c =>  c.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DepositTariffsEntity>()
                .HasOne(d => d.Bank)
                .WithMany(b => b.Depoosits)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
