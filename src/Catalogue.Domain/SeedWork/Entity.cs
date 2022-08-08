namespace Catalogue.Domain.SeedWork
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public void MarkAsDeleted()
        {
            DeletedAt = DateTimeOffset.UtcNow;
        }
    }
}
