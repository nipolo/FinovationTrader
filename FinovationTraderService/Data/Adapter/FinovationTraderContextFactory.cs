
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinovationTrader.Data.Adapter
{
    public class FinovationTraderContextFactory : IDesignTimeDbContextFactory<FinovationTraderContext>
    {
        public FinovationTraderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinovationTraderContext>();
            // TODO: Find better way to inject connection string
            optionsBuilder.UseSqlServer("Server=localhost;Database=master;User ID=sa;Password=1qaz!QAZ;");

            return new FinovationTraderContext(optionsBuilder.Options);
        }
    }
}
