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
        public IEnumerable<Product> Products { get; set; }

        public List<SoldProductsGroup> SoldProd { get; set; }

        public virtual ApplicationUser User { get; set; }
        public int NotSold = 0;

        public MyProductsViewModel(ApplicationDbContext context, ApplicationUser user)
        {
            SoldProd = (from p in context.Product
                        where p.User == user
                        join ot in context.OrderProduct
                        on p.ProductId equals ot.ProductId
                        join o in context.Order
                        on ot.OrderId equals o.OrderId
                        where o.PaymentType != null
                        group new { p, p.ProductId, ot } by new { p.Title, p.ProductId, p.Quantity } into grouped
                        select new SoldProductsGroup
                        {
                            ProdTitle = grouped.Key.Title,
                            ProdId = grouped.Key.ProductId,
                            ProdQuantity = grouped.Key.Quantity,
                            ProdCount = grouped.Select(g => g.ot.ProductId).Count()
                        }
                        ).ToList();

            //Products = from p in context.Product
            //           where p.User == user
            //           join s in SoldProd
            //           on p.ProductId equals s.ProdId
            //           group new { p.Title, p.Quantity, s.}
                       
            //           select p;

            //NotSold = from p in Products
            //          join s in SoldProd
            //          on 
        }
    }
}
