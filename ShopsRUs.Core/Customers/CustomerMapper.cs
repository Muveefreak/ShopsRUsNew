using ShopsRUs.Core.Customers.Commands;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Customers
{
    public static class CustomerMapper
    {
        public static Customer ToCreateEntity(this CreateCustomerCommand command)
        {
            var result = new Customer
            {
                CustomerName = command.CustomerName.ToLower(),
                CustomerType = command.CustomerType.ToLower()
            };

            return result;
        }

        public static CustomerResponse ToResponse(this Customer customer)
        {
            var result = new CustomerResponse
            {
                CustomerId = customer.Id,
                CustomerName = customer.CustomerName,
                CustomerType = customer.CustomerType,
                CreatedDate = customer.CreatedDate
            };

            return result;
        }
    }
}
