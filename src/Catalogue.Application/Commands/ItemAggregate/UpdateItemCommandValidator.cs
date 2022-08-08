using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Application.Commands.ItemAggregate
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(command => command.ItemId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();
            RuleFor(command => command.Price).GreaterThanOrEqualTo(0);
            RuleFor(command => command.ImageUrl).NotEmpty();
        }
    }
}
