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

        public List<Product> NotSold { get; set; }

        public List<ProdOnOrderGroup> ProdOnOrder { get; set; }

        public MyProductsViewModel(ApplicationDbContext context, ApplicationUser user)
        {
            // Query that returns customers products that have been sold
             SoldProd = (from p in context.Product // where there are products in product table
                        where p.User == user      // where the user on the product row is the same as current user
                        join ot in context.OrderProduct // join with the OrderProduct table
                        on p.ProductId equals ot.ProductId // where the productID is the same
                        join o in context.Order // then join this selected product with the Order table
                        on ot.OrderId equals o.OrderId // where the OrderId on OrderProduct is the same as OrderId on Order, select this product
                        where o.PaymentType != null // make sure order is completed with payment
                        group new { p, ot, p.ProductId } by new { p.Title, p.Quantity, p.ProductId } into grouped// create new object with product and OrderProduct, set Value and group
                        select new SoldProductsGroup // select the product that passed above conditions and make new object instance
                        {
                            ProdTitle = grouped.Key.Title, // set Title Property 
                            ProdId = grouped.Key.ProductId, // set Product Id Property
                            ProdCount = grouped.Select(g => g.ot.ProductId).Count(), // set the Count Property
                            ProdQuantity = grouped.Key.Quantity - grouped.Select(g => g.ot.ProductId).Count() // reduce the quantity by count sold
                            // from the grouped var, select each ProuductId in OrderProduct and Count the # of times it appears on the table
                        }
                        ).ToList(); // return as a list
            

            // Query that returns List of Customers Products that have not been sold or added to an order
            NotSold =
                (from p in context.Product //from products table
                 where p.User == user // where user is same as customer
                 where !context.OrderProduct.Any(prod => prod.ProductId == p.ProductId) // where the ProductId from Product Table does not exist on OrderProduct Table
                 select p).ToList(); // select that product


            // Query that returns list of products that are on an order but the order has not been completed
            ProdOnOrder = (from p in context.Product // from products on Product Table
                           where p.User == user // where user is the customer
                           join ot in context.OrderProduct // join with OrderProduct
                           on p.ProductId equals ot.ProductId // where the Product Tavle ProductId equals the ProductId on OrderProduct
                           join o in context.Order // join this Product with Order Table
                           on ot.OrderId equals o.OrderId // the the OrderId on the OrderProduct Table equls the OrderId on Order Table
                           where o.PaymentType == null // where the PaymentType is not completed
                           where !SoldProd.Any(s => s.ProdId == p.ProductId) // and where this Product does not exist on the SoldProd List
                           group new { p, o } by new { p.Title, p.Quantity, p.ProductId } into newGroup
                           select new ProdOnOrderGroup
                           {
                               ProdTitle = newGroup.Key.Title,
                               ProdQuantity = newGroup.Key.Quantity,
                               ProdCount = newGroup.Select(po => po.p.ProductId).Count()
                           }
                        
                        ).ToList(); // select these Products
        }
    }
}
