using Catalogue.Application.Queries.Common;

namespace Catalogue.Application.Queries.ItemQueries
{
    public interface IItemQueries
    {
        Task<IEnumerable<ItemDTO>> GetItemsAsync(string sortField, SortOrder sortOrder, int? skip, int? take, string? itemName);

        Task<ItemDetailsDTO> GetItemByIdAsync(int itemId);
    }
}
