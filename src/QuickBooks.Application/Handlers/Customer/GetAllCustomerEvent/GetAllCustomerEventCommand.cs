using AutoMapper;
using MediatR;
using QuickBooks.Application.Filters;
using QuickBooks.Application.Interfaces.QBFC;
using QuickBooks.Application.Interfaces.QBFC.Custom;
using QuickBooks.Application.Models.QBFC.Customer;
using QuickBooks.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Handlers.Customer.GetAllCustomerEvent
{
    public class GetAllCustomerEventCommand : 
        RequestParameter, IRequest<PagedResponse<GetAllCustomerEventDto>>
    {
        public string Search { get; set; }
    }

    public class GetAllCustomerEventCommandHandler :
        IRequestHandler<GetAllCustomerEventCommand, PagedResponse<GetAllCustomerEventDto>>
    {
        private readonly IGetAllCustomerQbfcMessage _qbfcMessage;
        private readonly IQbfcHandler _qbfcHandler;
        private readonly IMapper _mapper;

        public GetAllCustomerEventCommandHandler(
            IGetAllCustomerQbfcMessage qbfcMessage,
            IQbfcHandler qbfcHandler,
            IMapper mapper)
        {
            _qbfcMessage = qbfcMessage;
            _qbfcHandler = qbfcHandler;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetAllCustomerEventDto>> Handle(
            GetAllCustomerEventCommand request, CancellationToken cancellationToken)
        {
            var qbfcResponse = _qbfcHandler.Execute(_qbfcMessage);
            var dto = _mapper.Map<IEnumerable<GetAllCustomerEventDto>>(qbfcResponse);
            var pagedResponse = new PagedResponse<GetAllCustomerEventDto>(dto, request.PageNumber, request.PageSize);
            await Task.CompletedTask;
            return pagedResponse;
        }
    }
}
