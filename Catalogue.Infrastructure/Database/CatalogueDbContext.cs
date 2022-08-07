using Catalogue.Domain.SeedWork;
using Catalogue.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Infrastructure.Database
{
    public class CatalogueDbContext : DbContext, IUnitOfWork
    {
        public CatalogueDbContext(DbContextOptions<CatalogueDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ItemTypeConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
