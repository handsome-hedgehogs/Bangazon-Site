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
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;

namespace BangazonAuth.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _currentUser { get; set; }
        private IHostingEnvironment _environment;

        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = ctx;
            _environment = environment;
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

        public async Task<IActionResult> Detail([FromRoute]int? id)
        {
            // If no id was in the route, return 404
            if (id == null)
            {
                return NotFound();
            }

            // Create new instance of view model
            ProductDetailViewModel model = new ProductDetailViewModel();

            // Set the `Product` property of the view model
            model.Product = await _context.Product
                    .Include(prod => prod.User)
                    .SingleOrDefaultAsync(prod => prod.ProductId == id);

            // If product not found, return 404
            if (model.Product == null)
            {
                return NotFound();
            }

            return View(model);
        }
        
        //Author: Willie Pruitt
        //Filters and Displays List of products based on user input {searchString}
        public async Task<IActionResult> Search(string searchString)
        {
            ProductListViewModel model = new ProductListViewModel();


            model.Products = await _context.Product
                .ToListAsync();
            //If search param not empty or null, search if Product description or title contains input
            if (!string.IsNullOrEmpty(searchString))
            {
                model.Products = model.Products.Where(p => p.Description.Contains(searchString) || p.Title.Contains(searchString));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel(_context);

            // Get current user
            var user = await GetCurrentUserAsync();
            model.Product = new Product();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("product.User");
            ModelState.Remove("Product.PhotoURL");
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validation, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
                foreach(var file in model.Image)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filename = _environment.WebRootPath + $@"\products\{file.FileName.Split('\\').Last()}";
                    using(var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        model.Product.PhotoURL = $@"\products\{file.FileName.Split('\\').Last()}";
                    }
                }
                var user = await GetCurrentUserAsync();
                model.Product.User = user;

                _context.Add(model.Product);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ProductCreateViewModel model2 = new ProductCreateViewModel(_context);
            return View(model2);
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

        public IActionResult Error()
        {
            return View();
        }
    }
}