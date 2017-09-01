using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BangazonAuth.Models;
using BangazonAuth.Models.AccountViewModels;
using BangazonAuth.Models.ManageViewModels;
using Microsoft.EntityFrameworkCore.Metadata;

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

            // Auto generating dates written by Jackie Knight and Eliza Meeks
            // Table constraints written by Eliza Meeks

            // Payment type auto generating dates
            builder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            // Product type constraints
            builder.Entity<ProductType>()
                .HasMany(o => o.Products)
                .WithOne(l => l.ProductType)
                .OnDelete(DeleteBehavior.Restrict);

            //Order auto generating dates and constraints
            builder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(l => l.Order)
                .OnDelete(DeleteBehavior.Restrict);

            //Product builder constraints and auto generating dates
            builder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");
            builder.Entity<Product>()
                .HasMany(o => o.OrderProducts)
                .WithOne(l => l.Product)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>()
                .HasMany(o => o.Recommended)
                .WithOne(l => l.Product)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>()
                .HasMany(o => o.UserLiked)
                .WithOne(l => l.Product)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>()
                .HasMany(o => o.Ratings)
                .WithOne(l => l.Product)
                .OnDelete(DeleteBehavior.Restrict);

            //Application user constraints
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.PaymentTypes)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Products)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Ratings)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.UserLiked)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.RecommendedByMe)
                .WithOne(o => o.Recommender)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.RecommendedToMe)
                .WithOne(o => o.Recommendee)
                .OnDelete(DeleteBehavior.Restrict);
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