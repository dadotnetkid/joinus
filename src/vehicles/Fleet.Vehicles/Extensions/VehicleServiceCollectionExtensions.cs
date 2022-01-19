using System.Reflection;
using Fleet.Vehicles.Models;
using Fleet.Vehicles.Repositories;
using Fleet.Vehicles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fleet.Vehicles.Extensions
{
    public static class VehicleServiceCollectionExtensions
    {
        public static IServiceCollection AddVehicleService(this IServiceCollection services, IConfiguration configuration, Assembly containingAssembly)
        {
            services.Configure<VehicleDbContextOptions>(configuration);
            services.AddDbContext<VehicleDbContext>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<VehicleDbContextOptions>>();
                options.UseSqlite(config.Value.ConnectionString, sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly(containingAssembly.FullName);
                });
            });

            services.AddScoped<IVehicleRepository, EfCoreVehicleRepository>();
            services.AddScoped<IVehicleLogItemRepository, EfCoreVehicleLogItemRepository>();
            services.AddScoped<IFleetRepository, EfCoreFleetRepository>();

            services.AddScoped<IVehicleService, DefaultVehicleService>();
            services.AddScoped<IFleetService, DefaultFleetService>();

            return services;
        }
    }
}
