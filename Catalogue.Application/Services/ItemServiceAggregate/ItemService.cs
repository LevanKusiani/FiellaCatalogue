using Catalogue.Domain.Entities.ItemAggregate;

namespace Catalogue.Application.Services.ItemServiceAggregate
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync(string name, string description, decimal? priceFrom, decimal? priceTo)
        {
            var items = await _repository.GetAllAsync(name, description, priceFrom, priceTo);

            return items.Select(x => new ItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl
            });
        }

        public async Task<ItemDTO> GetItemByIdAsync(int id)
        {
            var result = new ItemDTO();

            var item = await _repository.FindByIdAsync(id);

            if (item != null)
            {
                result = new ItemDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl
                };
            }

            return result;
        }
    }
}
