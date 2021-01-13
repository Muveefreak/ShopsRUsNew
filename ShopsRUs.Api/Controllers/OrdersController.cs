using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.Configuration;
using ShopsRUs.Core.Orders.Commands;
using ShopsRUs.Core.Orders.Queries;

namespace ShopsRUs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllOrdersByCustomerId")]
        public async Task<IActionResult> GetAllOrdersByCustomerId([FromQuery] long customerId)
        {
            var query = new GetAllOrdersByCustomerIdQuery(customerId);
            var result = await _mediator.Send(query);

            if (result.isSuccess)
            {
                return Ok(new ApiResponse
                {
                    ResponseCode = "00",
                    ResponseDescription = result.message,
                    Data = result.response
                });
            }
            return NotFound(new ApiResponse { ResponseCode = "01", ResponseDescription = result.message, Data = null });
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.isSuccess)
            {
                return Ok(new ApiResponse
                {
                    ResponseCode = "00",
                    ResponseDescription = result.message,
                    Data = result.response
                });
            }
            return NotFound(new ApiResponse { ResponseCode = "01", ResponseDescription = result.message, Data = null });
        }
    }
}
