using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data
{
    public class SolarDbContext : IdentityDbContext
    {
        public SolarDbContext() { }

        public SolarDbContext(DbContextOptions<SolarDbContext> options) 
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
