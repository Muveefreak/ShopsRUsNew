using MediatR;
using ShopsRUs.Core.Discounts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Discounts.Queries
{
    public class GetAllDiscountsQuery : IRequest<(List<DiscountResponse> response, string message, bool isSuccess)>
    {
    }
}
