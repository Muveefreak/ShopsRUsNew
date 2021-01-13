using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.Configuration;
using ShopsRUs.Core.Customers.Commands;
using ShopsRUs.Core.Customers.Queries;

namespace ShopsRUs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
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

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromQuery] long customerId)
        {
            var query = new GetCustomerByIdQuery(customerId);
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

        [HttpGet]
        [Route("GetCustomerByName")]
        public async Task<IActionResult> GetCustomerByName([FromQuery] string customerName)
        {
            var query = new GetCustomerByNameQuery(customerName);
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
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.isSuccess)
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
