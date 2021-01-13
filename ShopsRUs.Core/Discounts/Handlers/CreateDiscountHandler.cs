using MediatR;
using ShopsRUs.Core.Discounts.Commands;
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
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, (DiscountResponse response, string message, bool isSuccess)>
    {
        private readonly ShopsRUsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateDiscountHandler(ShopsRUsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<(DiscountResponse response, string message, bool isSuccess)> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountsQuery = new GetAllDiscountsQuery();
            var discountsList = await _mediator.Send(discountsQuery);
            if (!discountsList.isSuccess)
            {
                return (null, "Unable to complete request, please try again later.", false);
            }

            List<String> conditions = discountsList.response.Select(z => z.DiscountType).ToList();

            var customerDiscount = conditions.FirstOrDefault(x => x.ToLower().Trim() == request.DiscountType.ToLower().Trim());
            if (customerDiscount != null)
            {
                String join = String.Join(",", conditions);
                return (null, $"Discount type already exists", false);
            }

            var discount = request.ToCreateEntity();

            _dbContext.Discounts.Add(discount);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = discount.ToResponse();

            return (response, "Discount type successfully created", true);
        }
    }
}
