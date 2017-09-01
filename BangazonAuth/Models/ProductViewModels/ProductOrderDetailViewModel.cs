using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BangazonAuth.Models.ProductViewModels
{
    public class ProductOrderDetailViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public double Total { get; set; }

        public  ProductOrderDetailViewModel(int? id, ApplicationDbContext Context)
        {

            Products =  (from op in Context.OrderProduct
                              where op.OrderId == id
                              join p in Context.Product
                              on op.ProductId equals p.ProductId
                              join pt in Context.ProductType
                              on p.ProductTypeId equals pt.ProductTypeId
                              select p).ToList();

            Total = (from x in Products select x.Price).Sum();

        }

        //public ProductOrderDetailViewModel() { }


    }
}
