
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WorkFinder.Models;

// Only "Worker" role can access this controller
[RoleAuthorize("Worker")]
public class wEditProfileController : Controller
{
    private readonly HhandDbContext _context;

    public wEditProfileController(HhandDbContext context)
    {
        _context = context;
    }
    // WorkProvider page info showing
    public IActionResult WorkProvider()
    {
        var workProvider = _context.WorkProviders.ToList(); // fetch all workers
        return View(workProvider); // send to view
    }

    //Tomal linq new
    [HttpGet]
    public async Task<IActionResult> Index(string? profession, string? location)
    {
        var workprovider = from w in _context.WorkProviders select w;

        if (!string.IsNullOrEmpty(profession))
        {
            workprovider = workprovider.Where(w => w.WorkerNeed.ToLower() == profession.ToLower());
        }

        if (!string.IsNullOrEmpty(location))
        {
            workprovider = workprovider.Where(w => w.Location.ToLower() == location.ToLower());
        }

        return View(await workprovider.ToListAsync());
    }


    //Tomal linq new ends


    // for linq adding this

    //[HttpGet]
    //public async Task<IActionResult> Index(string? profession)
    //{
    //    var workprovider = from w in _context.WorkProviders select w;

    //    if (!string.IsNullOrEmpty(profession))
    //    {
    //        workprovider = workprovider.Where(w => w.WorkerNeed.ToLower() == profession.ToLower());
    //    }

    //    return View(await workprovider.ToListAsync());
    //}

    // ends here linq






    // GET: Worker Dashboard
    public IActionResult Dashboard()
    {
        var workers = _context.WorkerTs.ToList();
        return View(workers); // Ensure Views/wEditProfile/Dashboard.cshtml exists
    }

    // GET: Worker Profile
    public IActionResult Profile()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var worker = _context.WorkerTs.FirstOrDefault(w => w.Id == userId);
        if (worker == null)
            return NotFound();

        return View(worker); // Ensure Views/wEditProfile/Profile.cshtml exists
    }

    // GET: Update Profile
    [HttpGet]
    public async Task<IActionResult> UpdateProfile()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Account");

        var worker = await _context.WorkerTs.FindAsync(userId);
        if (worker == null)
            return NotFound();

        return View("Profile", worker); // Reuse the Profile view for editing
    }

    // POST: Update Profile
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(WorkerT worker)
    {
        if (!ModelState.IsValid)
            return View("Profile", worker);

        var existing = await _context.WorkerTs.FindAsync(worker.Id);
        if (existing == null)
            return NotFound();

        // Update only allowed fields
        existing.Name = worker.Name;
        existing.Gender = worker.Gender;
        existing.Location = worker.Location;
        existing.Email = worker.Email;
        existing.Password = worker.Password;
        existing.Age = worker.Age;
        existing.Salary = worker.Salary;
        existing.Experienced = worker.Experienced;
        existing.ContactNumber = worker.ContactNumber;

        await _context.SaveChangesAsync();

        TempData["Success"] = "Profile updated successfully.";
        return RedirectToAction("Profile");
    }
}
