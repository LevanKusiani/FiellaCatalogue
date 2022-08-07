using Catalogue.Domain.Entities.ItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogue.Infrastructure.Database.Configurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            var entity = builder.ToTable(nameof(Item));

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.Name);

            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.Description).HasMaxLength(3_000);
            entity.Property(x => x.ImageUrl).HasMaxLength(1_000);
        }
    }
}
