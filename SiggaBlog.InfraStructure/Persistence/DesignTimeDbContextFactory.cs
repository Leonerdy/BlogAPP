using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SiggaBlog.InfraStructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SiggaBlogDbContext>
    {
        public SiggaBlogDbContext CreateDbContext(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Combine(Environment.GetFolderPath(folder), "Test");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dbPath = Path.Combine(path, "siggaBlog.db");

            var optionsBuilder = new DbContextOptionsBuilder<SiggaBlogDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new SiggaBlogDbContext(optionsBuilder.Options);
        }
    }
}