using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure.Entities
{
    public class Discount
    {
        public long DiscountId { get; set; }
        public string DiscountType { get; set; }
        public float? DiscountAmount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string IsPercentageType { get; set; }

    }
}
