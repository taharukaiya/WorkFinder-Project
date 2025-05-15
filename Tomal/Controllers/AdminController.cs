using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Models;

namespace Tomal.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminController : Controller
    {
        private readonly HhandDbContext _context;

        public AdminController(HhandDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AllWorkers()
        {
            var workers = _context.WorkerTs.ToList();
            return View(workers);
        }

        public IActionResult AllProviders()
        {
            var providers = _context.WorkProviders.ToList();
            return View(providers);
        }
    }

}


