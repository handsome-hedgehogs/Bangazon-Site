using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Models;
using BangazonAuth.Data;
using BangazonAuth.Models.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;

namespace BangazonAuth.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _currentUser;
        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Index()
        {
            // Create new instance of the view model
            ProductListViewModel model = new ProductListViewModel();

            // Set the properties of the view model
            model.Products = await _context.Product.ToListAsync();
            return View(model);

        }

        [Authorize]
        public async Task<IActionResult> CannotAddToOrder()
        {
            // If no id was in the route, return 404
            ApplicationUser user = await GetCurrentUserAsync();

            return View(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }
            _currentUser = await GetCurrentUserAsync();
            // Create new instance of view model
            ProductDetailViewModel model = new ProductDetailViewModel(_currentUser, _context, id);

            // If product not found, return 404
            if (model.Product == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //Written by: Eliza Meeks
        //Adds products to order fromt he product detail view
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(Product product)
        {
            var user = await GetCurrentUserAsync();
            ProductDetailViewModel model = new ProductDetailViewModel(_currentUser, _context, product.ProductId);

            ApplicationUser productSeller = _context.Product.Where(p => p.ProductId == product.ProductId).Select(p => p.User).First();
            if (productSeller == user)
            {
                return RedirectToAction("CannotAddToOrder", product.ProductId);
            }
            ModelState.Remove("product.User");



            Order existingOrder = _context.Order.SingleOrDefault(o => o.PaymentTypeId == null && o.User == user);
            if (existingOrder == null)
            {
                Order newOrder = new Order() { User = user };
                _context.Order.Add(newOrder);
                OrderProduct newOrderProduct = new OrderProduct() { OrderId = newOrder.OrderId, ProductId = product.ProductId };
                _context.OrderProduct.Add(newOrderProduct);
            }
            else
            {
                OrderProduct newOrderProduct = new OrderProduct() { OrderId = existingOrder.OrderId, ProductId = product.ProductId };
                _context.OrderProduct.Add(newOrderProduct);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Author: Willie Pruitt
        //Filters and Displays List of products based on user input {searchString}
        public async Task<IActionResult> Search(string searchBy, string searchString)
        {
            ProductListViewModel model = new ProductListViewModel();
            model.Products = await _context.Product
                .ToListAsync();
            //If search param not empty or null, search if Product description or title contains input
            if (!string.IsNullOrEmpty(searchString) && searchBy.Equals("Product"))
            {
                model.Products = model.Products.Where(p => p.Description.ToLower().Contains(searchString.ToLower()) || p.Title.ToLower().Contains(searchString.ToLower()));
            }
            else if (!string.IsNullOrEmpty(searchString) && searchBy.Equals("LocalDelivery"))
            {
                model.Products = model.Products.Where(p => p.LocalDelivery.Equals(true) && p.Location.ToLower().Contains(searchString.ToLower()));
            }
            return View(model);
        }
        //Author: Willie Pruitt
        //Filters and Displays List of products based on user input {searchString}
        public async Task<IActionResult> OfType(string searchString)
        {
            ProductListViewModel model = new ProductListViewModel();
            //If search param not empty or null, search for Products with Product type equal to input
            if (!string.IsNullOrEmpty(searchString))
            {
                model.Products = await _context.Product.Where(p => p.ProductType.Label.Equals(searchString)).ToListAsync();
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel(_context, _currentUser);

            // Get current user
            var user = await GetCurrentUserAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("product.User");

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validation, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
                var user = await GetCurrentUserAsync();
                product.User = user;

                _context.Add(product);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ProductCreateViewModel model = new ProductCreateViewModel(_context, _currentUser);
            return View(model);
        }



        public async Task<IActionResult> Types()
        {
            var model = new ProductTypesViewModel();

            // Get line items grouped by product id, including count
            var counter = from product in _context.Product
                          group product by product.ProductTypeId into grouped
                          select new { grouped.Key, myCount = grouped.Count() };

            // Build list of Product Type instances for display in view
            model.ProductTypes = await (from type in _context.ProductType
                                        join a in counter on type.ProductTypeId equals a.Key
                                        select new ProductType
                                        {
                                            ProductTypeId = type.ProductTypeId,
                                            Label = type.Label,
                                            Quantity = a.myCount
                                        }).ToListAsync();

            return View(model);
        }


        [HttpGet]
        [Authorize]
        // Method that uses ViewModel MyProductsViewModel to handle DB query and Class SoldProductsGroup
        // Returns a List of Products that can display for a customer how many items still in stock and 
        // how many have been sold
        // Authored by : Jackie Knight && Tamela Lerma
        public async Task<IActionResult> MyProducts()
        {
            var user = await GetCurrentUserAsync();
            // create VM instance and pass in DBContext along with current user
            MyProductsViewModel myProducts = new MyProductsViewModel(_context, user);                          
            return View(myProducts);                        
        }



        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProductFromOrderConfirmed(int OrderId, int ProductId)
        {
            var user = await GetCurrentUserAsync();

            var orderProduct = await _context.OrderProduct.FirstAsync(m => m.ProductId == ProductId && m.OrderId == OrderId);

            _context.OrderProduct.Remove(orderProduct);         
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Orders");
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}