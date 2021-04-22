using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinovationTrader.Data.Adapter;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinovationTrader.Data.Module
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<FinovationTraderContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("TradersDB"),
                    providerOptions => providerOptions
                        .EnableRetryOnFailure(maxRetryCount: 5)
                    ));

            services.AddScoped<DbContext, FinovationTraderContext>();

            return services;
        }
    }
}
