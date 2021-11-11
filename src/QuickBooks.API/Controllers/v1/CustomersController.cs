using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBooks.API.Controllers.Base;
using QuickBooks.Application.Handlers.Customer.GetAllCustomerEvent;
using QuickBooks.Application.Handlers.Customer.GetCustomerByIdEvent;
using QuickBooks.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace QuickBooks.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class CustomersController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<GetAllCustomerEventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCustomerAsync(
            [FromQuery] GetAllCustomerEventCommand command,
            CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCustomerByIdEventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerByIdAsync(
          string id,
          CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdEventCommand(id), cancellationToken));
        }
    }
}
