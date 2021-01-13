using MediatR;
using ShopsRUs.Core.Configuration;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using ShopsRUs.Core.Discounts.Queries;
using ShopsRUs.Core.Discounts.Responses;
using ShopsRUs.Core.Orders.Interfaces;
using ShopsRUs.Core.Orders.Queries;
using ShopsRUs.Core.Orders.Responses;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Core.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<(float response, string message, bool isSuccess)> GetTotalInvoice(long customerId, CancellationToken cancellationToken)
        {
            var customerDetailsQuery = new GetCustomerByIdQuery(customerId);
            var customerDetails = await _mediator.Send(customerDetailsQuery);

            if(!customerDetails.isSuccess)
            {
                return (0f, $"Unable to complete request \n{customerDetails.message}", false);
            }

            var customerOrdersQuery = new GetAllOrdersByCustomerIdQuery(customerId);
            var customerOrders = await _mediator.Send(customerOrdersQuery);

            if (!customerOrders.isSuccess)
            {
                return (0f, $"Unable to complete request \n{customerOrders.message}", false);
            }

            var discountsQuery = new GetAllDiscountsQuery();
            var discounts = await _mediator.Send(discountsQuery);

            if (!discounts.isSuccess)
            {
                return (0f, $"Unable to complete request \n{discounts.message}", false);
            }


            var discountManager = new DiscountManager(customerDetails, customerOrders, discounts);
            var totalInvoiceWithDiscount = discountManager.GetDiscountedTotal();

            return (totalInvoiceWithDiscount, "Success", true);
        }
    }

    public class OrderManager
    {
        public OrderManager(CustomerResponse customer, List<OrderResponse> orders)
        {
            Customer = customer;
            Orders = orders;
        }

        public List<OrderResponse> Orders { get; set; }
        public CustomerResponse Customer { get; set; }

        public float Total() => Orders.Sum(x => x.Amount);
    }

    public class DiscountManager
    {
        public DiscountManager((CustomerResponse response, string message, bool isSuccess) customer, (List<OrderResponse> response, 
            string message, bool isSuccess) orders, (List<DiscountResponse> response, string message, bool isSuccess) discounts)
        {
            Customer = customer.response;
            Orders = orders.response;
            Discounts = discounts.response;
        }

        public List<OrderResponse> Orders { get; set; }
        public CustomerResponse Customer { get; set; }
        public List<DiscountResponse> Discounts { get; set; }
        private float totalSum { get; set; }



        public float GetDiscountedTotal()
        {
            this.totalSum = Orders.Sum(x => x.Amount);
            float totalDiscountedAmount = 0f;
            float totalDiscountForPercentage = 0f;
            float totalDiscountForNonPercentage = 0f;

            var customerDiscount = Discounts.FirstOrDefault(x => x.DiscountType.ToLower().Trim() == Customer.CustomerType.ToLower().Trim());

            if(customerDiscount.IsPercentageType == "Y")
            totalDiscountForPercentage = GetDiscountForPercentage(customerDiscount); // Get Discount Sum from Percentage discount

            
            totalDiscountForNonPercentage = GetDiscountForNonPercentage(customerDiscount); // Get Discount Sum from Non Percentage discount


            totalDiscountedAmount = totalSum - (totalDiscountForNonPercentage + totalDiscountForPercentage);
            return totalDiscountedAmount;
        }

        public float GetDiscountForNonPercentage(DiscountResponse customerDiscount)
        {
            // Discount Price break
            float totalDiscountForNonPercentage = 0f;
            var discountType = "price break";
            var constantDiscount = Discounts.FirstOrDefault(x => x.DiscountType.ToLower().Trim() == discountType.ToLower().Trim());

            var valInHundreds = (float)Math.Floor((decimal)this.totalSum / 100);
            totalDiscountForNonPercentage = (float)constantDiscount.DiscountAmount * valInHundreds;

            return totalDiscountForNonPercentage;
        }

        public float GetDiscountForPercentage(DiscountResponse customerDiscount)
        {
            int percentageCount = 0; // Number Of Percentage Discounts Customer Qualifies For
            float totalDiscountForPercentage = 0f;

            // customer Type Discount ie Affilliate or Employee
            if (customerDiscount != null && percentageCount == 0)
            {
                percentageCount++;
                foreach (var order in Orders)
                {
                    if (!AppSettings.ExemptedItems.Contains(order.OrderType.ToLower()))
                    {
                        var discountedAmount = order.Amount * ((float)customerDiscount.DiscountPercent / 100);
                        totalDiscountForPercentage += discountedAmount;
                    }
                }
            }

            // Loyalty Discount
            if (Customer.GetAge() > AppSettings.LoyaltyYears && percentageCount == 0)
            {
                percentageCount++;
                foreach (var order in Orders)
                {
                    if (!AppSettings.ExemptedItems.Contains(order.OrderType.ToLower()))
                    {
                        var discountedAmount = order.Amount * ((float)customerDiscount.DiscountPercent / 100);
                        totalDiscountForPercentage += discountedAmount;
                    }
                }
            }
            return totalDiscountForPercentage;
        }
    }


}
