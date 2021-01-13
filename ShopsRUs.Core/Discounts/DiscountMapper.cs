using ShopsRUs.Core.Discounts.Commands;
using ShopsRUs.Core.Discounts.Responses;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Discounts
{
    public static class DiscountMapper
    {
        public static Discount ToCreateEntity(this CreateDiscountCommand command)
        {
            var result = new Discount
            {
                DiscountType = command.DiscountType.ToLower(),
                DiscountPercentage = command.DiscountPercentage,
                IsPercentageType = command.IsPercentageType,
                DiscountAmount = command.DiscountAmount,
            };

            return result;
        }

        public static DiscountResponse ToResponse(this Discount discount)
        {
            var result = new DiscountResponse
            {
                DiscountId = discount.DiscountId,
                DiscountPercent = discount.DiscountPercentage.Value,
                DiscountType = discount.DiscountType,
                IsPercentageType = discount.IsPercentageType,
                DiscountAmount = Convert.ToInt32(discount.DiscountAmount),
            };

            return result;
        }
    }
}
