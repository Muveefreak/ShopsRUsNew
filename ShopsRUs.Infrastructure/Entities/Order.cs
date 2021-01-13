using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopsRUs.Infrastructure.Entities
{
    [Table("Orders")]
    public class Order
    {
        public long OrderId { get; set; }
        public string ItemName { get; set; }
        public float Amount { get; set; }
        public string OrderType { get; set; }
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public string OrderStatus { get; set; }
    }
}
