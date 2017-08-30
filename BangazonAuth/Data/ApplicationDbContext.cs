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
            //builder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.HasKey(e => e.Id)
            //    .HasName("Id");
            //    entity.ToTable("Id", "dbo");
            //    entity.Property(e => e.Id).UseSqlServerIdentityColumn();
            //    entity.ForSqlServerToTable("CustomerId");
            //});
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

      
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
   
        public DbSet<Rating> Rating { get; set; }
    }
}
