using MediatR;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ShopsRUs.Core.Customers.Handlers
{
    public class GetCustomerByNameHandler : IRequestHandler<GetCustomerByNameQuery, (CustomerResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;

        public GetCustomerByNameHandler(ShopsRUsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(CustomerResponse response, string message, bool isSuccess)> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            var customerEntity = _dbContext.Customers
                .FirstOrDefault(x => x.CustomerName == request.CustomerName);

            var response = customerEntity?.ToResponse();

            if (response == null)
            {
                return (null, "Customer does not exist", false);
            }

            return (response, "Successful", true);
        }
    }
}
