using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonAuth.Models.ProductViewModels;
using BangazonAuth.Data;
using Microsoft.EntityFrameworkCore;

namespace BangazonAuth.Controllers
{
    public class HomeController : Controller
    {
        private  ApplicationDbContext _context;

        public HomeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        //Author: Willie Pruitt
        //Filters and Displays List of last 20 created products
        public async Task<IActionResult> Index()
        {
            ProductListViewModel model = new ProductListViewModel();

            model.Products = await _context.Product.OrderByDescending(p => p.DateCreated).Take(count: 20).ToListAsync();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
