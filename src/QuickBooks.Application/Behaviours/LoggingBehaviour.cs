using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.Application.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        //private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour()
        {
           // _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            //_logger.LogInformation("Request {Name}", requestName);

            return Task.CompletedTask;
        }
    }
}
