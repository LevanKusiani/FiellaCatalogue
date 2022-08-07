using FluentValidation;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();
            RuleFor(command => command.Price).GreaterThan(0);
            RuleFor(command => command.ImageUrl).NotEmpty();
        }
    }
}
