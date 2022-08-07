namespace Catalogue.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task AddAsync(T entity);

        Task<T> FindByIdAsync<TId>(TId id);

        void Update(T entity);
    }
}
