using ShopsRUs.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Customers.Responses
{
    public class CustomerResponse
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GetAge()
        {
            return CreatedDate.CalculateAge();
        }
    }
}
