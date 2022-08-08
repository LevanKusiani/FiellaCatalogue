using Catalogue.Domain.Entities.ItemAggregate;
using Catalogue.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Infrastructure.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(CatalogueDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Item> GetActiveItemAsync(int id)
        {
            return await _dbContext.Set<Item>()
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
        }

        public async Task<IEnumerable<Item>> GetAllAsync(string name, string description, decimal? priceFrom, decimal? priceTo)
        {
            return await _dbContext.Set<Item>()
                .AsNoTracking()
                .Where(x =>
                        (string.IsNullOrEmpty(name) || (x.Name.Contains(name)))
                    || (string.IsNullOrEmpty(description) || x.Description.Contains(description))
                    || (priceFrom == null || x.Price >= priceFrom)
                    || (priceTo == null || x.Price <= priceTo))
                .ToListAsync();
        }
    }
}
