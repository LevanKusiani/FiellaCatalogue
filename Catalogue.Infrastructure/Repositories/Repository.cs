using Catalogue.Domain.SeedWork;
using Catalogue.Infrastructure.Database;

namespace Catalogue.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity, IAggregateRoot
    {
        protected readonly CatalogueDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;

        public Repository(CatalogueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<T> FindByIdAsync<TId>(TId id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
