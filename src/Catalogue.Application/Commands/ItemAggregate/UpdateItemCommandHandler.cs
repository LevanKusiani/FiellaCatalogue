using Catalogue.Domain.Entities.ItemAggregate;
using MediatR;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
    {
        private readonly IItemRepository _repository;

        public UpdateItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetActiveItemAsync(request.ItemId);

            if (item == null)
                return false;

            item.UpdateDetails(request.Name, request.Description, request.Price, request.ImageUrl);

            _repository.Update(item);

            await _repository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
