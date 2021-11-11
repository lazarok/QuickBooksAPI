using FluentValidation;
using Microsoft.Extensions.Localization;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerByName;
using QuickBooks.Resources.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.AddCustomerEvent
{
    public class AddCustomerEventCommandValidator : AbstractValidator<AddCustomerEventCommand>
    {
        private readonly IQbfcHandler _qbfcHandler;
        private readonly IGetCustomerByNameQbfcMessage _getCustomerByName;
        private readonly IStringLocalizer<I18n> _localizer;

        public AddCustomerEventCommandValidator(
            IQbfcHandler qbfcHandler,
            IGetCustomerByNameQbfcMessage getCustomerByName,
            IStringLocalizer<I18n> localizer)
        {
            _qbfcHandler = qbfcHandler;
            _getCustomerByName = getCustomerByName;
            _localizer = localizer;

            RuleFor(p => p.Name)
                  .NotEmpty().WithMessage("'{PropertyName}' is required.")
                  .NotNull()
                  .MaximumLength(41).WithMessage("'{PropertyName}' must not exceed 41 characters.")
                  .MustAsync(IsUniqueName).WithMessage("'{PropertyName}' already exists.");
        }

        private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var request = new GetCustomerByNameRequest(name);
            var response = _qbfcHandler.Execute(request, _getCustomerByName);
            return response == default;
        }
    }
}
