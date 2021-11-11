using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using QuickBooks.Application.Exceptions;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Customer;
using QuickBooks.Application.Models.QBFC.Customer.GetCustomerById;
using QuickBooks.Resources.Infrastructure;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.GetCustomerByIdEvent
{
    public record GetCustomerByIdEventCommand(string Id) : IRequest<GetCustomerByIdEventDto>;

    public class GetCustomerByIdEventCommandHandler :
        IRequestHandler<GetCustomerByIdEventCommand, GetCustomerByIdEventDto>
    {
        private readonly IGetCustomerByIdQbfcMessage _qbfcMessage;
        private readonly IQbfcHandler _qbfcHandler;
        private readonly IStringLocalizer<I18n> _localizer;
        private readonly IMapper _mapper;

        public GetCustomerByIdEventCommandHandler(
            IGetCustomerByIdQbfcMessage qbfcMessage,
            IQbfcHandler qbfcHandler,
            IStringLocalizer<I18n> localizer,
            IMapper mapper)
        {
            _qbfcMessage = qbfcMessage;
            _qbfcHandler = qbfcHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdEventDto> Handle(
            GetCustomerByIdEventCommand request, CancellationToken cancellationToken)
        {
            var qbfcRequest = _mapper.Map<GetCustomerByIdRequest>(request);
            var qbfcResponse = _qbfcHandler.Execute(qbfcRequest, _qbfcMessage);
            var dto = _mapper.Map<GetCustomerByIdEventDto>(qbfcResponse);           
            if (dto == default) 
                throw new ApiException(_localizer["NotFound"], HttpStatusCode.NotFound);
            await Task.CompletedTask;
            return dto;
        }
    }
}
