using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBooks.API.Controllers.Base;
using QuickBooks.Application.Handlers.Customer.GetAllCustomerEvent;
using QuickBooks.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomerController: BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<GetAllCustomerEventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetVaccinationDataAsync(
            [FromQuery] GetAllCustomerEventCommand command, 
            CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
