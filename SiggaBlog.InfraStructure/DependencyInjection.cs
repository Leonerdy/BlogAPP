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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IJsonPlaceholderService, JsonPlaceholderService>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            DbContextOptions options)
        {
            return services
                .AddInfrastructure()
                .AddSingleton(
                    new SiggaBlogDbContext(
                        (DbContextOptions<SiggaBlogDbContext>)options));
        }
    }
}
