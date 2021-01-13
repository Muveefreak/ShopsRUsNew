using MediatR;
using ShopsRUs.Core.Discounts.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Discounts.Commands
{
    public class CreateDiscountCommand : IRequest<(DiscountResponse response, string message, bool isSuccess)>
    {
        public string DiscountType { get; }
        public int DiscountAmount { get; }
        public int DiscountPercentage { get; }
        public string IsPercentageType { get; }

        public CreateDiscountCommand(int discountAmount, string discountType, int discountPercentage, string isPercentageType)
        {
            DiscountAmount = discountAmount;
            DiscountType = discountType;
            DiscountPercentage = discountPercentage;
            IsPercentageType = isPercentageType;
        }
    }
}
