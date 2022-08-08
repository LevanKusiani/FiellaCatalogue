using FluentValidation;

namespace Catalogue.Application.Commands.ItemAggregate
{
    internal class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(command => command.ItemId).NotEmpty();
        }
    }
}
