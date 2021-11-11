using FluentValidation;
using Microsoft.Extensions.Localization;
using QuickBooks.Resources.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.UpdateCustomerEvent
{
    public class UpdateCustomerEventCommandValidator : AbstractValidator<UpdateCustomerEventCommand>
    {
        private readonly IStringLocalizer<I18n> _localizer;

        public UpdateCustomerEventCommandValidator(
            IStringLocalizer<I18n> localizer)
        {
            _localizer = localizer;

            RuleFor(p => p.EditSequence)
                  .NotEmpty().WithMessage("'{PropertyName}' is required.")
                  .NotNull()
                  .MaximumLength(16).WithMessage("'{PropertyName}' must not exceed 16 characters.");
        }
    }
}
