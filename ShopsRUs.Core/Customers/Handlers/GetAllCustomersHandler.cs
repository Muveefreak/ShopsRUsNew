using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Customers.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, (List<CustomerResponse> response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;

        public GetAllCustomersHandler(ShopsRUsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<CustomerResponse> response, string message, bool isSuccess)> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _dbContext.Customers.ToListAsync(cancellationToken: cancellationToken);

            var responses = customers.Select(x => x.ToResponse()).ToList();

            if(responses.Count < 1)
            {
                return (null, "Failed", false);
            }

            return (responses, "Successful", true);
        }
    }
}
