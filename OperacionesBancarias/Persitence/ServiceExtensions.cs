using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persitence.Contexts;
using Persitence.Repository;

namespace Persitence
{
    public static class ServiceExtensions
    {
        public static void AddPersitenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("Persitence")));

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(OperacionBancariaRepositoryAsync<>));
            #endregion

        }
    }
}
