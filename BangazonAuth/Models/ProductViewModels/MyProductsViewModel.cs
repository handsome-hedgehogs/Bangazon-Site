using BangazonAuth.Classes;
using BangazonAuth.Data;
using System.Collections.Generic;
using System.Linq;

// ViewModel used in ProductsController for MyProducts Method
// Model returns a List of new Class type SoldProductsGroup
// queried DB for a sold products that belong to a customer
// the method MyProducts is calle from Layout View navigation
// Authored by : Jackie Knight & Tamela Lerma
namespace BangazonAuth.Models.ProductViewModels
{
    public class MyProductsViewModel
    {
        // List of a new type SoldProductGroup, this class is located in Classes/SoldProductGroup
        public List<SoldProductsGroup> SoldProd { get; set; }

        public MyProductsViewModel(ApplicationDbContext context, ApplicationUser user)
        {
            SoldProd = (from p in context.Product // where there are products in product table
                        where p.User == user      // where the user on the product row is the same as current user
                        join ot in context.OrderProduct // join with the OrderProduct table
                        on p.ProductId equals ot.ProductId // where the productID is the same
                        join o in context.Order // then join this selected product with the Order table
                        on ot.OrderId equals o.OrderId // where the OrderId on OrderProduct is the same as OrderId on Order, select this product
                        where o.PaymentType != null // make sure order is completed with payment
                        group new { p, ot } by new { p.Title, p.Quantity } into grouped// create new object with product and OrderProduct, set Value and group
                        select new SoldProductsGroup // select the product that passed above conditions and make new object instance
                        {
                            ProdTitle = grouped.Key.Title, // set Title Property 
                            ProdQuantity = grouped.Key.Quantity, // set quantity property
                            ProdCount = grouped.Select(g => g.ot.ProductId).Count() // set the Count Property
                            // from the grouped var, select each ProuductId in OrderProduct and Count the # of times it appears on the table
                        }
                        ).ToList(); // return as a list

        }
    }
}
