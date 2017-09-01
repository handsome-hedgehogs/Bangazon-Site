using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// View Model to handle Order Detail View using Controller OrderCntroller
// Method it is obstantiated in is CompletedOrderDetail
// Requires instace of DBContext and OrderID ro be passsed in
// View that displays returned information is CompletedOrderDetail.cshtml
// Authored by : Tamela Lerma
namespace BangazonAuth.Models.ProductViewModels
{
    public class ProductOrderDetailViewModel
    {
        // return a list of products
        public IEnumerable<Product> Products { get; set; }

        // hold value of price totaled
        public double Total { get; set; }

        // retrieves orders from OrderProd Table where the ID matches
        // from there it joins with the Product table and retrieves pproducts that are in the order
        // then joins where the productTypeId from the Products Table matches the ProductTypeId on ProductType table
        public  ProductOrderDetailViewModel(int? id, ApplicationDbContext Context)
        {

            Products =  (from op in Context.OrderProduct
                              where op.OrderId == id
                              join p in Context.Product
                              on op.ProductId equals p.ProductId
                              join pt in Context.ProductType
                              on p.ProductTypeId equals pt.ProductTypeId
                              select p).ToList();
            // select the Price property from the Products returned in list and add them together
            Total = (from x in Products select x.Price).Sum();

        }

    }
}
