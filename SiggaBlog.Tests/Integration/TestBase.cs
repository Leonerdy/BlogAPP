using Microsoft.EntityFrameworkCore;
using SiggaBlog.InfraStructure.Persistence;
using System;

namespace SiggaBlog.Tests.Integration
{
    public abstract class TestBase : IDisposable
    {
        protected readonly SiggaBlogDbContext _dbContext;

        protected TestBase()
        {
            var options = new DbContextOptionsBuilder<SiggaBlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new SiggaBlogDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

        public virtual void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
} 