using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopsRUs.Core.Discounts.Commands;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Discounts.Validators
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            List<string> percentageTypeConditions = new List<string>() { "Y", "N" };
            String percentageJoin = String.Join(",", percentageTypeConditions);

            RuleFor(x => x.DiscountType)
                .NotEmpty().WithMessage("Discount Type is required.");

            RuleFor(x => x.IsPercentageType)
                .NotEmpty().WithMessage("Percentage Type is required.");

            RuleFor(x => x.IsPercentageType)
                .Must(x => percentageJoin.Contains(x))
                .WithMessage($"Please only pass: {percentageJoin} as IsPercentageType.");

            RuleFor(x => x.DiscountPercentage)
                .NotEmpty().When(x => x.IsPercentageType == "Y").WithMessage("Discount Percentage is required.")
                .GreaterThan(0).When(x => x.IsPercentageType == "Y").WithMessage("Discount Percentage is required and must be greater than 0.")
                .LessThan(100).When(x => x.IsPercentageType == "Y").WithMessage("Discount Percentage must be less than 100.");

            RuleFor(x => x.DiscountAmount)
                .NotNull().When(x => x.IsPercentageType == "N").WithMessage("Discount Amount is required.")
                .GreaterThan(0).When(x => x.IsPercentageType == "N").WithMessage("Discount Amount is required and must be greater than 0");

        }
    }
}
