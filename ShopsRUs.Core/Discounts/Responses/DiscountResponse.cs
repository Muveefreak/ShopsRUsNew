using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Discounts.Responses
{
    public class DiscountResponse
    {
        public long DiscountId { get; set; }
        public string DiscountType { get; set; }
        public int? DiscountAmount { get; set; }
        public int DiscountPercent { get; set; }
        public string IsPercentageType { get; set; }
        //public string PercentageDiscount { get; set; }
    }
}
