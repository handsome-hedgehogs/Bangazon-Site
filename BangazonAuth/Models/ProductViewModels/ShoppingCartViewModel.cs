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
        public List<ProductInOrder> ProductsInOrder { get; set; }

        public Order Order { get; set; }

        public ApplicationUser User { get; set; }

        public ShoppingCartViewModel(ApplicationDbContext ctx, ApplicationUser currentUser)
        {
            User = currentUser;
            Order = ctx.Order.SingleOrDefault(o => o.PaymentType == null && o.User == User);
            if (Order == null){
                Order = new Order() { User = User, DateCreated = DateTime.Now };
            }
            var initialProducts = (
                from p in ctx.Product
                join op in ctx.OrderProduct on p.ProductId equals op.ProductId
                where op.OrderId == Order.OrderId
                select p
                ).ToList();

            ProductsInOrder = new List<ProductInOrder>();

            foreach (var item in initialProducts)
            {
                var newProductInOrder = new ProductInOrder() { ProductId = item.ProductId, Title = item.Title, Price = item.Price };

                var inOrder = ProductsInOrder.Where(p => p.ProductId == newProductInOrder.ProductId);
                var NumberInOrder = inOrder.Count();
                
                if (NumberInOrder > 0)
                {
                    var itsInOrder = ProductsInOrder.Where(p => p.ProductId == newProductInOrder.ProductId).First();
                    var index = ProductsInOrder.IndexOf(itsInOrder);
                    ProductsInOrder[index].Quantity = ProductsInOrder[index].Quantity + 1;
                    ProductsInOrder[index].Price = ProductsInOrder[index].Price + newProductInOrder.Price;
                } else
                {
                    newProductInOrder.Quantity = 1;
                    ProductsInOrder.Add(newProductInOrder);
                }
                
            }
           
        }
    }
}
