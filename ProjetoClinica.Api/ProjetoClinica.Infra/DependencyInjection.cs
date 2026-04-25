using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoClinica.Extensions.DependencyInjection;
using ProjetoClinica.Infra.Data;
using System.Reflection;

namespace ProjetoClinica.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            services.AddDependencies(Assembly.GetAssembly(typeof(DependencyInjection)));
            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql
                (
                     configuration.GetConnectionString("DefaultConnection"),
                     ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                )
            );

            services.AddHealthChecks().AddDbContextCheck<DatabaseContext>(tags: ["ready"]);

            return services;
        }
    }
}
