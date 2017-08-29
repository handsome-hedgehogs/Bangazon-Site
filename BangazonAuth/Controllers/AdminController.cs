using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Models;
using BangazonAuth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace BangazonAuth.Controllers
{
    [AuthorizeAttribute(Roles="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Index()
        {
            return View();
        }
    }

}
