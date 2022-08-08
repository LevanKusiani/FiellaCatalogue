using Catalogue.Domain.Entities.ItemAggregate;
using Catalogue.Domain.SeedWork;
using MediatR;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly IItemRepository _repository;

        public DeleteItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetActiveItemAsync(request.ItemId);

            if (item == null)
                return false;

            item.MarkAsDeleted();

            _repository.Update(item);

            await _repository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
