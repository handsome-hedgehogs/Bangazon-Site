using BangazonAuth.Classes;
using BangazonAuth.Data;
using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models.ProductViewModels
{
    public class MyProductsViewModel
    {
        public List<SoldProductsGroup> SoldProd { get; set; }

        public MyProductsViewModel(ApplicationDbContext context, ApplicationUser user)
        {
            SoldProd = (from p in context.Product
                        where p.User == user
                        join ot in context.OrderProduct
                        on p.ProductId equals ot.ProductId
                        join o in context.Order
                        on ot.OrderId equals o.OrderId
                        where o.PaymentType != null
                        group new { p, ot } by new { p.Title, p.Quantity } into grouped
                        select new SoldProductsGroup
                        {
                            ProdTitle = grouped.Key.Title,
                            ProdQuantity = grouped.Key.Quantity,
                            ProdCount = grouped.Select(g => g.ot.ProductId).Count()
                        }
                        ).ToList();

        }
    }
}
