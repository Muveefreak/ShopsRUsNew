using MediatR;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Orders.Commands;
using ShopsRUs.Core.Orders.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Orders.Handlers
{

    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, (OrderResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateOrderHandler(ShopsRUsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<(OrderResponse response, string message, bool isSuccess)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var customerIdQuery = new GetCustomerByIdQuery(request.CustomerId);
            var customerExist = await _mediator.Send(customerIdQuery);
            if (customerExist.response == null)
            {
                return (null, "No Customer with the requested Id exists", false);
            }
            var order = request.ToCreateEntity();

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = order.ToResponse();

            return (response, "Order Type is successfully created", true);
        }
    }
}
