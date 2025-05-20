using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.InfraStructure.Persistence;
using SiggaBlog.InfraStructure.Repositories;
using SiggaBlog.InfraStructure.Services;

namespace SiggaBlog.InfraStructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbPath)
        {
            services.AddDbContext<SiggaBlogDbContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
            });

            services.AddHttpClient();
            services.AddScoped<JsonPlaceholderService>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
