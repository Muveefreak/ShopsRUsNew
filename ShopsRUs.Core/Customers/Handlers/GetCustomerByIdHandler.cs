using MediatR;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Customers.Handlers
{

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, (CustomerResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;

        public GetCustomerByIdHandler(ShopsRUsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(CustomerResponse response, string message, bool isSuccess)> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customerEntity = await _dbContext.Customers
                .FindAsync(request.CustomerId);

            var response = customerEntity?.ToResponse();

            if (response == null)
            {
                return (null, "Customer does not exist", false);
            }

            return (response, "Successful", true);
        }
    }
}
