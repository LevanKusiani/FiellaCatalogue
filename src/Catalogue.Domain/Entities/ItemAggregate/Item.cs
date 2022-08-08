using Catalogue.Domain.SeedWork;

namespace Catalogue.Domain.Entities.ItemAggregate
{
    public class Item : Entity, IAggregateRoot
    {
        public Item(string name,
            string description,
            decimal price,
            string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public string ImageUrl { get; private set; }

        public void UpdateDetails(string name,
            string description,
            decimal price,
            string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }
    }
}
