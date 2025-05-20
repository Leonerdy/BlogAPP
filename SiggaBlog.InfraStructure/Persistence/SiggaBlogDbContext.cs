using Microsoft.EntityFrameworkCore;
using SiggaBlog.Domain.Entities;

namespace SiggaBlog.InfraStructure.Persistence
{
    public class SiggaBlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public SiggaBlogDbContext(DbContextOptions<SiggaBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades
            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Album>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Photo>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Todo>()
                .HasKey(t => t.Id);

            // Configuração do User e suas entidades relacionadas
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Address, a =>
                {
                    a.OwnsOne(ad => ad.Geo);
                });

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Company);
        }
    }
}
