using Catalogue.Domain.SeedWork;

namespace Catalogue.Domain.Entities.ItemAggregate
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item> GetActiveItemAsync(int id);

        Task<IEnumerable<Item>> GetAllAsync(string name, string description, decimal? priceFrom, decimal? priceTo);
    }
}
