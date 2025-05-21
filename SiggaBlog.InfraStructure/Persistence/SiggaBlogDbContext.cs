using Microsoft.EntityFrameworkCore;
using SiggaBlog.Domain.Entities;

namespace SiggaBlog.InfraStructure.Persistence
{
    public class SiggaBlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
       
        public SiggaBlogDbContext(DbContextOptions<SiggaBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades
            //modelBuilder.Entity<Post>()
            //    .HasKey(p => p.UserId);
                        
            //modelBuilder.Entity<Comment>()
            //    .HasKey(c => c.PostId);
             
        }
    }
}
