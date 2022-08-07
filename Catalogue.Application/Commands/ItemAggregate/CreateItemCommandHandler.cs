using Catalogue.Domain.Entities.ItemAggregate;
using MediatR;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, bool>
    {
        private readonly IItemRepository _itemRepository;

        public CreateItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task<bool> Handle(CreateItemCommand command, CancellationToken cancellationToken)
        {
            var item = new Item(command.Name,
                command.Description,
                command.Price,
                command.ImageUrl);

            await _itemRepository.AddAsync(item);

            await _itemRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
