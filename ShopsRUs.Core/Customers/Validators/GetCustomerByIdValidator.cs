using FluentValidation;
using ShopsRUs.Core.Customers.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Customers.Validators
{
    public class GetCustomerByIdValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdValidator()
        {
            RuleFor(x => x.CustomerId)
               .NotNull()
               .NotEmpty().WithMessage("Id is required.");
        }
    }
}
