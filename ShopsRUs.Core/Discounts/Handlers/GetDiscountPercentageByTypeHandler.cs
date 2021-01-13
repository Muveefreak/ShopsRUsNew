using MediatR;
using ShopsRUs.Core.Discounts.Queries;
using ShopsRUs.Core.Discounts.Responses;
using ShopsRUs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ShopsRUs.Core.Discounts.Handlers
{
    public class GetDiscountPercentageByTypeHandler : IRequestHandler<GetDiscountPercentageByTypeQuery, (DiscountResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;

        public GetDiscountPercentageByTypeHandler(ShopsRUsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(DiscountResponse response, string message, bool isSuccess)> Handle(GetDiscountPercentageByTypeQuery request, CancellationToken cancellationToken)
        {

            var discountEntity = _dbContext.Discounts
               .FirstOrDefault(x => x.DiscountType.ToLower() == request.DiscountType.ToLower());

            var response = discountEntity?.ToResponse();

            if (response == null)
            {
                return (null, "Discount type does not exist", false);
            }

            return (response, "Successful", true);
        }
    }

    
}
