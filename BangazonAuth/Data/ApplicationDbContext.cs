using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BangazonAuth.Models;
using BangazonAuth.Models.AccountViewModels;
using BangazonAuth.Models.ManageViewModels;

namespace BangazonAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Written by Jackie Knight and Eliza Meeks
            // Makes tables autogenerate dates.
            builder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
        }
      
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
   
        public DbSet<Rating> Rating { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }
    }
}
