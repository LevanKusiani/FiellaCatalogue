using MediatR;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class CreateItemCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
