using Microsoft.EntityFrameworkCore;
using ShopsRUs.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopsRUsDbContext(DbContextOptions<ShopsRUsDbContext> options)
            : base(options)
        {
        }
    }
}
