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

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentType.ToListAsync());
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await GetCurrentUserAsync();
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .Where(m => m.User == user).ToListAsync();
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            return View();
        }

        // POST: PaymentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType payment)
        {
            payment.DateCreated = DateTime.Now;
            ModelState.Remove("User");
            
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                payment.User = user;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Manage");
            }
            return View(payment);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentTypeId,DateCreated,Description,AccountNumber")] PaymentType paymentType)
        {

            if (id != paymentType.PaymentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentType.PaymentTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeId == id);
            _context.PaymentType.Remove(paymentType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeId == id);
        }
    }
}
