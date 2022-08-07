namespace Catalogue.Application.Services.ItemServiceAggregate
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync(string name, string description, decimal? priceFrom, decimal? priceTo);

        Task<ItemDTO> GetItemByIdAsync(int id);
    }
}
