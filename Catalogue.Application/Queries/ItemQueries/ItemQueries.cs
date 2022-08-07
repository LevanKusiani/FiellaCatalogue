using Catalogue.Application.Configurations;
using Catalogue.Application.Queries.Common;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Text;

namespace Catalogue.Application.Queries.ItemQueries
{
    public class ItemQueries : IItemQueries
    {
        private readonly string _connectionString = string.Empty;

        public ItemQueries(IOptions<ConnectionStrings> config)
        {
            _connectionString = config?.Value.CatalogueDbConnection ?? throw new ArgumentNullException(nameof(ConnectionStrings));
        }

        public async Task<ItemDetailsDTO> GetItemByIdAsync(Guid itemId)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            connection.Open();

            return await connection.QuerySingleAsync<ItemDetailsDTO>($@"
                SELECT * FROM ""public"".""Item""
                WHERE ""Id"" = @id",
                new { itemId });
        }

        public async Task<IEnumerable<ItemDTO>> GetItemsAsync(string sortField, SortOrder sortOrder, int? skip, int? take, string? itemName)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            connection.Open();

            var filters = new StringBuilder();
            var offset = new StringBuilder();

            if (!string.IsNullOrEmpty(itemName))
                filters.AppendLine(@"AND ""ItemName"" LIKE CONCAT('%', LOWER(@itemName), '%')");

            if (skip != null && take != null)
                offset.AppendLine(@"OFFSET @skip LIMIT @take");

            return await connection.QueryAsync<ItemDTO>($@"
                SELECT * FROM ""public"".""Item""
                ORDER BY ""{sortField}"" {sortOrder} 
                {offset}",
                new { itemName });
        }
    }
}
