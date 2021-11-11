using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using QuickBooks.Application.Exceptions;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.UpdateCustomer;
using QuickBooks.Application.Parameters;
using QuickBooks.Resources.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.UpdateCustomerEvent
{
    public record UpdateCustomerEventCommand : IRequest<UpdateCustomerEventDto>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
        public string EditSequence { get; set; }

        public UpdateCustomerEventCommand(string id, UpdateCustomerEventParameter parameter)
        {
            Id = id;
            Name = parameter?.Name;
            CompanyName = parameter?.CompanyName;
            Phone = parameter?.Phone;
            Email = parameter?.Email;
            Contact = parameter?.Contact;
            Notes = parameter?.Notes;
            IsActive = parameter?.IsActive;
            EditSequence = parameter.EditSequence;
        }
    }

    public class UpdateCustomerEventHandler :
      IRequestHandler<UpdateCustomerEventCommand, UpdateCustomerEventDto>
    {
        private readonly IUpdateCustomerQbfcMessage _qbfcMessage;
        private readonly IQbfcHandler _qbfcHandler;
        private readonly IStringLocalizer<I18n> _localizer;
        private readonly IMapper _mapper;

        public UpdateCustomerEventHandler(
            IUpdateCustomerQbfcMessage qbfcMessage,
            IQbfcHandler qbfcHandler,
            IStringLocalizer<I18n> localizer,
            IMapper mapper)
        {
            _qbfcMessage = qbfcMessage;
            _qbfcHandler = qbfcHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerEventDto> Handle(
            UpdateCustomerEventCommand request, CancellationToken cancellationToken)
        {
            var qbfcRequest = _mapper.Map<UpdateCustomerRequest>(request);
            var qbfcResponse = _qbfcHandler.Execute(qbfcRequest, _qbfcMessage);
            var dto = _mapper.Map<UpdateCustomerEventDto>(qbfcResponse);
            if (dto == default)
                throw new ApiException(_localizer["NotFound"], HttpStatusCode.NotFound);
            await Task.CompletedTask;
            return dto;
        }
    }
}
