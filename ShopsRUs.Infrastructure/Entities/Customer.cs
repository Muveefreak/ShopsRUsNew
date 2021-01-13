using ShopsRUs.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public ICollection<Order> Orders { get; private set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int GetAge()
        {
            return CreatedDate.CalculateAge();
        }
    }
}
