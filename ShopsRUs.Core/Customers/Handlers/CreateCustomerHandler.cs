using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Customers.Commands;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Core.Discounts.Queries;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Customers.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, (CustomerResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateCustomerHandler(ShopsRUsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<(CustomerResponse response, string message, bool isSuccess)> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var discountsQuery = new GetAllDiscountsQuery();
            var discountsList = await _mediator.Send(discountsQuery);
            if (!discountsList.isSuccess)
            {
                return (null, "Unable to complete request, please try again later.", false);
            }
            List<String> conditions = discountsList.response.Select(z => z.DiscountType).ToList();

            var customerDiscount = conditions.FirstOrDefault(x => x.ToLower().Trim() == request.CustomerType.ToLower().Trim());
            if(customerDiscount == null && conditions.Count > 0)
            {
                String join = String.Join(",", conditions);
                return (null, $"Please only pass: {join} as Customer types.", false);
            }

            if (conditions.Count == 0)
            {
                request.CustomerType = "";
            }

            var customerNameQuery = new GetCustomerByNameQuery(request.CustomerName);
            var customerExist = await _mediator.Send(customerNameQuery);
            if (customerExist.response != null)
            {
                return (null, "Customer Name already exists", false);
            }

            var customer = request.ToCreateEntity();

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = customer.ToResponse();


            return (response, "Successfully created customer", true);
        }
    }
}
