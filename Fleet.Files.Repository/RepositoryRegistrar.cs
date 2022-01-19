using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fleet.Files.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fleet.Files.Repository
{
    public static class RepositoryRegistrar
    {
        public static void AddFilesRepository(this IServiceCollection services, string connectionString, Assembly containingAssembly)
        {
            services.AddDbContext<FileDbContext>((serviceProvider, options) =>
            {
                options.UseSqlite(connectionString, sqliteOptions =>
                {
                });
            });
            services.AddScoped<IFilesRepository, FilesRepository>();
        }
    }

}
