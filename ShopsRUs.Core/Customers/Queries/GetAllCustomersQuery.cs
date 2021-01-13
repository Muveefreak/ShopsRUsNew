using MediatR;
using ShopsRUs.Core.Customers.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Customers.Queries
{
    public class GetAllCustomersQuery: IRequest<(List<CustomerResponse> response, string message, bool isSuccess)>
    {
    }
}
