using MediatR;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class DeleteItemCommand : IRequest<bool>
    {
        public int ItemId { get; set; }
    }
}
