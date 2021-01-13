using FluentValidation;
using ShopsRUs.Core.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            List<string> orderStatusConditions = new List<string>() { "U", "P" }; // Paid or Unpaid
            String join = String.Join(",", orderStatusConditions);

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(x => x.ItemName)
                .NotEmpty().WithMessage("Item name is required.");

            RuleFor(x => x.OrderType)
                .NotEmpty().WithMessage("Order type is required.");

            RuleFor(x => x.OrderStatus)
                .NotEmpty().WithMessage("Order Status is required.");

            //RuleFor(x => x.OrderStatus)
            //    .Must(x => join.Contains(x))
            //    .WithMessage($"Please only pass: {join} as Is Order Type.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
