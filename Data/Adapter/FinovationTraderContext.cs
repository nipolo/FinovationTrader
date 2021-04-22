using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinovationTrader.Data.States;

using Microsoft.EntityFrameworkCore;

namespace FinovationTrader.Data.Adapter
{
    public class FinovationTraderContext : DbContext
    {
        public FinovationTraderContext(DbContextOptions<FinovationTraderContext> options)
            : base(options)
        {

        }

        public DbSet<Trader> Traders { get; set; }

        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trader>()
            .HasKey(t => t.Id);

            modelBuilder.Entity<Trader>()
                .HasMany(t => t.Cryptocurrencies)
                .WithOne()
                .HasForeignKey(t => t.TraderId);

            modelBuilder.Entity<Cryptocurrency>()
                .HasKey(t => t.Symbol);
        }
    }
}
