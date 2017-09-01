using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BangazonAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using BangazonAuth.Data;

namespace BangazonAuth.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var userstore = new UserStore<ApplicationUser>(context);

                if (!context.Roles.Any(r => r.Name == "Administrator"))
                {
                    var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                    await roleStore.CreateAsync(role);
                }

                if (!context.Roles.Any(r => r.Name == "Member"))
                {
                    var role = new IdentityRole { Name = "Member", NormalizedName = "Member" };
                    await roleStore.CreateAsync(role);
                }

                if (!context.ApplicationUser.Any(u => u.FirstName == "admin"))
                {

                  //This method will be called after migrating to the latest version.
                  ApplicationUser user = new ApplicationUser
                  {
                      FirstName = "admin",
                      LastName = "admin",
                      StreetAddress = "123 Infinity Way",
                      UserName = "admin@admin.com",
                      NormalizedUserName = "ADMIN@ADMIN.COM",
                      Email = "admin@admin.com",
                      NormalizedEmail = "ADMIN@ADMIN.COM",
                      EmailConfirmed = true,
                      LockoutEnabled = false,
                      SecurityStamp = Guid.NewGuid().ToString("D")
                  };
                    var passwordHash = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
                    await userstore.CreateAsync(user);
                    await userstore.AddToRoleAsync(user, "Administrator");


                    var users = new ApplicationUser[]
                    {
                    new ApplicationUser
                    {
                        FirstName = "Jackie-O",
                        LastName = "Nassy",
                        StreetAddress = "123 Infinity Way",
                        UserName = "JK@gmail.com",
                        NormalizedUserName = "JK@GMAIL.COM",
                        Email = "JK@gmail.com",
                        NormalizedEmail = "JK@GMAIL.COM",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    },
                    new ApplicationUser
                    {
                        FirstName = "Willie",
                        LastName = "One-Hell-of-a-Butler",
                        StreetAddress = "123 Infinity Way",
                        UserName = "CoolButler@gmail.com",
                        NormalizedUserName = "COOLBUTLER@GMAIL.COM",
                        Email = "CoolButler@gmail.com",
                        NormalizedEmail = "COOLBUTLER@GMAIL.COM",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    },
                    new ApplicationUser
                    {
                        FirstName = "Eliza",
                        LastName = "I've-Got-This-Meeks",
                        StreetAddress = "123 Infinity Way",
                        UserName = "EZ@gmail.com",
                        NormalizedUserName = "EZ@GMAIL.COM",
                        Email = "EZ@gmail.com",
                        NormalizedEmail = "EZ@GMAIL.COM",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    } };
                foreach (var p in users)
                {
                    var passwordHash2 = new PasswordHasher<ApplicationUser>();
                    p.PasswordHash = passwordHash2.HashPassword(p, "Admin8*");
                    await userstore.CreateAsync(p);
                    await userstore.AddToRoleAsync(p, "Member");
                }
                };

                // Look for any products.
                if (!context.PaymentType.Any())
                {
                    var paymentTypes = new PaymentType[]
                    {
                    new PaymentType {
                        Description = "Visa",
                        AccountNumber = "102939475751",
                        User = context.ApplicationUser.Single(u => u.Email == "admin@admin.com")
                    },
                    new PaymentType {
                        Description = "Amex",
                        AccountNumber = "756483920187",
                        User = context.ApplicationUser.Single(u => u.Email == "admin@admin.com")
                    }
                    };

                    foreach (PaymentType i in paymentTypes)
                    {
                        context.PaymentType.Add(i);
                    }
                    context.SaveChanges();
                }



                if (!context.ProductType.Any())
                {
                    var productTypes = new ProductType[]
                    {
                    new ProductType {
                        Label = "Electronics"
                    },
                    new ProductType {
                        Label = "Appliances"
                    },
                    new ProductType {
                        Label = "Sporting Goods"
                    },
                    new ProductType {
                        Label = "Housewares"
                    },
                    new ProductType {
                        Label = "Food"
                    }
                    };

                    foreach (ProductType i in productTypes)
                    {
                        context.ProductType.Add(i);
                    }
                    context.SaveChanges();
                }





                if (!context.Product.Any())
                {
                    var products = new Product[]
                    {
                    new Product {
                        Title = "Kite",
                        Quantity = 300,
                        Description = "It flies high",
                        Price = 9.99,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductType = context.ProductType.Single(t => t.Label == "Sporting Goods"),
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Curtains",
                        Quantity = 200,
                        Description = "They make it dark",
                        Price = 140.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Macbook Pro",
                        Quantity = 100,
                        Description = "It's powerful",
                        Price = 1278.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Electronics").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Refrigerator",
                        Quantity = 56,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Appliances").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Grapes",
                        Quantity = 39987,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Food").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Watermelon",
                        Quantity = 890,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Food").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Orange",
                        Quantity = 78,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Appliances").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Bananas",
                        Quantity = 369,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Food").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Kiwi",
                        Quantity = 6,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Food").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Apples",
                        Quantity = 2000,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Food").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Lamp",
                        Quantity = 89,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "EZ@gmail.com")
                    },
                    new Product {
                        Title = "Rug",
                        Quantity = 547,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "EZ@gmail.com")
                    },
                    new Product {
                        Title = "Couch",
                        Quantity = 190,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "EZ@gmail.com")
                    },
                    new Product {
                        Title = "Table",
                        Quantity = 29,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "EZ@gmail.com")
                    },
                    new Product {
                        Title = "Shower Curtain",
                        Quantity = 345,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Housewares").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "EZ@gmail.com")
                    },
                    new Product {
                        Title = "Baseball",
                        Quantity = 1239,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Bat",
                        Quantity = 10009,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Soccer Ball",
                        Quantity = 3,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "Tennis ball",
                        Quantity = 38,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    new Product {
                        Title = "golf balls",
                        Quantity = 148,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "CoolButler@gmail.com")
                    },
                    new Product {
                        Title = "Basketball",
                        Quantity = 100,
                        Description = "It keep things cool",
                        Price = 1149.00,
                        Location = "Nashville",
                        LocalDelivery = true,
                        PhotoURL = "hey.jpg",
                        ProductTypeId = context.ProductType.Single(t => t.Label == "Sporting Goods").ProductTypeId,
                        User = context.ApplicationUser.Single(u => u.Email == "JK@gmail.com")
                    },
                    };

                    foreach (Product i in products)
                    {
                        context.Product.Add(i);
                    }

                    context.SaveChanges();
                }

            }
        }
    }
}