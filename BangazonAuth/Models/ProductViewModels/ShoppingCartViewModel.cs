using BangazonAuth.Data;
using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Controllers;
using Microsoft.AspNetCore.Identity;

namespace BangazonAuth.Models.ProductViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public Order Order { get; set; }

        public ApplicationUser User { get; set; }

        public ShoppingCartViewModel(ApplicationDbContext ctx, ApplicationUser currentUser)
        {
            User = currentUser;
            Order = ctx.Order.SingleOrDefault(o => o.PaymentType == null && o.User == User);
            if (Order == null){
                Order = new Order() { User = User, DateCreated = DateTime.Now };
            }
            Products = (
                from p in ctx.Product
                join op in ctx.OrderProduct on p.ProductId equals op.ProductId
                where op.OrderId == Order.OrderId
                select p
                ).ToList();
        }
    }
}
