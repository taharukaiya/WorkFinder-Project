


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tomal.Models;
using WorkFinder.Models;

// Only "WorkProvider" role can access this controller
[RoleAuthorize("WorkProvider")]

public class wProviderEditProfileController : Controller
{
    private readonly HhandDbContext _context;

    public wProviderEditProfileController(HhandDbContext context)
    {
        _context = context;
    }


    //linq tomal 
    [HttpGet]
    public async Task<IActionResult> Index(string? profession, string? location)
    {
        var workers = from w in _context.WorkerTs select w;

        if (!string.IsNullOrEmpty(profession))
        {
            workers = workers.Where(w => w.Experienced.ToLower() == profession.ToLower());
        }

        if (!string.IsNullOrEmpty(location))
        {
            workers = workers.Where(w => w.Location.ToLower() == location.ToLower());
        }

        return View(await workers.ToListAsync());
    }


    // linq tomal ends

    // for linq adding this

    //[HttpGet]
    //public async Task<IActionResult> Index(string? profession)
    //{
    //    var workers = from w in _context.WorkerTs select w;

    //    if (!string.IsNullOrEmpty(profession))
    //    {
    //        workers = workers.Where(w => w.Experienced.ToLower() == profession.ToLower());
    //    }

    //    return View(await workers.ToListAsync());
    //}

    // ends here linq

    //workR page showing data
    public IActionResult Worker()
    {
        var workers = _context.WorkerTs.ToList(); // fetch all workers
        return View(workers); // send to view
    }

    // GET: WorkProvider Dashboard
    //public IActionResult Dashboard()
    //{
    //    var providers = _context.WorkProviders.ToList();
    //    return View(providers); // Views/wProviderEditProfile/Dashboard.cshtml
    //}

    public IActionResult Dashboard()
    {
        var role = HttpContext.Session.GetString("UserRole");
        ViewBag.DebugRole = role; // ✅ Add this

        var providers = _context.WorkProviders.ToList();
        return View(providers);
    }


    // GET: WorkProvider Profile
    public IActionResult Profile()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var provider = _context.WorkProviders.FirstOrDefault(p => p.Id == userId);
        if (provider == null)
            return NotFound();

        return View(provider); // Views/wProviderEditProfile/Profile.cshtml
    }

    // GET: Update Profile
    [HttpGet]
    public async Task<IActionResult> UpdateProfile()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var provider = await _context.WorkProviders.FindAsync(userId);
        if (provider == null)
            return NotFound();

        return View("Profile", provider); // Reuse Profile view for editing
    }

    // POST: Update Profile
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(WorkProvider provider)
    {
        if (!ModelState.IsValid)
            return View("Profile", provider);

        var existing = await _context.WorkProviders.FindAsync(provider.Id);
        if (existing == null)
            return NotFound();

        // Update only allowed fields
        existing.Name = provider.Name;
        existing.Gender = provider.Gender;
        existing.Location = provider.Location;
        existing.Email = provider.Email;
        existing.Password = provider.Password;
        existing.WorkerNeed = provider.WorkerNeed;
        existing.ContactNumber = provider.ContactNumber;

        await _context.SaveChangesAsync();

        TempData["Success"] = "Profile updated successfully.";
        return RedirectToAction("Profile");
    }
    //

   
    //


}
