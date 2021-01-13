using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Core.Configuration;
using ShopsRUs.Core.Orders.Interfaces;

namespace ShopsRUs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IOrderService _invoiceService;

        public InvoiceController(IOrderService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("GetTotalInvoiceAmountByCustomerId")]
        public async Task<IActionResult> GetTotalInvoiceAmountByCustomerId([FromQuery] long customerId)
        {
            var result = await _invoiceService.GetTotalInvoice(customerId, new CancellationTokenSource().Token);

            
            return Ok(new ApiResponse
            {
                ResponseCode = "00",
                ResponseDescription = result.message,
                Data = result.response
            });
        }
    }
}
