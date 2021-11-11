using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using QuickBooks.Application.Exceptions;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.AddCustomer;
using QuickBooks.Resources.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.AddCustomerEvent
{
    public record AddCustomerEventCommand(
        string Name,
        string CompanyName,
        string Phone,
        string Email,
        string Contact,
        string Notes)
        : IRequest<AddCustomerEventDto>;

    public class AddCustomerEventHandler :
       IRequestHandler<AddCustomerEventCommand, AddCustomerEventDto>
    {
        private readonly IAddCustomerQbfcMessage _qbfcMessage;
        private readonly IQbfcHandler _qbfcHandler;
        private readonly IStringLocalizer<I18n> _localizer;
        private readonly IMapper _mapper;

        public AddCustomerEventHandler(
            IAddCustomerQbfcMessage qbfcMessage,
            IQbfcHandler qbfcHandler,
            IStringLocalizer<I18n> localizer,
            IMapper mapper)
        {
            _qbfcMessage = qbfcMessage;
            _qbfcHandler = qbfcHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<AddCustomerEventDto> Handle(
            AddCustomerEventCommand request, CancellationToken cancellationToken)
        {
            var qbfcRequest = _mapper.Map<AddCustomerRequest>(request);
            var qbfcResponse = _qbfcHandler.Execute(qbfcRequest, _qbfcMessage);
            var dto = _mapper.Map<AddCustomerEventDto>(qbfcResponse);
            if (dto == default)
                throw new ApiException();
            await Task.CompletedTask;
            return dto;
        }
    }
}
