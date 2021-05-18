using Finovation.Core.Application;
using Finovation.Core.Application.Abstractions;

using FinovationTrader.Application.Services;
using FinovationTrader.Data.Module;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinovationTrader.Application.Module
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataModule(configuration);

            services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
            services.AddSingleton<IFileStorageService, FileStorageService>();
            services.AddSingleton<ICryptoService, CryptoService>();

            return services;
        }
    }
}
