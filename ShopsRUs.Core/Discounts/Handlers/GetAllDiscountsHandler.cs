using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Discounts.Queries;
using ShopsRUs.Core.Discounts.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Discounts.Handlers
{
    class GetAllDiscountsHandler : IRequestHandler<GetAllDiscountsQuery, (List<DiscountResponse> response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;

        public GetAllDiscountsHandler(ShopsRUsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<DiscountResponse> response, string message, bool isSuccess)> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discounts = await _dbContext.Discounts.ToListAsync(cancellationToken: cancellationToken);

            var responses = discounts.Select(x => x.ToResponse()).ToList();

            if (responses.Count < 1)
            {
                return (null, "Failed", false);
            }

            return (responses, "Successful", true);
        }
    }
}
