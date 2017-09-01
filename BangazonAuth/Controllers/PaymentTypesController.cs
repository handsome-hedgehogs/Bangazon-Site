using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BangazonAuth.Models;
using BangazonAuth.Data;
using Microsoft.AspNetCore.Identity;
using BangazonAuth.Models.ManageViewModels;
using BangazonAuth.Controllers;

namespace BangazonAuth.Controllers
{
    public class PaymentTypesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }


        // GET: PaymentTypes/Details/5
        // Authored by : Tamela Lerma
        public async Task<IActionResult> Details(string id)
        {
            var user = await GetCurrentUserAsync();
            if (id == null)
            {
                return NotFound();
            }
            // Where User is equal to current user, return a list
            // used on Detail.cshtml 
            var paymentType = await _context.PaymentType
                .Where(m => m.User == user).ToListAsync();
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        // Authored by: Tamela Lerma
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            return View();
        }

        // POST: PaymentTypes/Create
        // Authored by : Tamela Lerma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType payment)
        {
            // create new date
            payment.DateCreated = DateTime.Now;
            //must remove User from Model to insert current user
            // The model being passed in has null for User
            ModelState.Remove("User");
            
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                payment.User = user;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                // redirect to view Index for ManageControler
                return RedirectToAction("Index", "Manage");
            }
            return View(payment);
        }


        // GET: PaymentTypes/Delete/5
        // pass in PaymenTypeId
        // Authored by : Tamela Lerma
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        // Delete from table using PaymentTypeId
        // redirect to Index.cshtml using ManageController upon completion
        // Authored by : Tamela Lerma
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            _context.PaymentType.Remove(paymentType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Manage");
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeId == id);
        }
    }
}
