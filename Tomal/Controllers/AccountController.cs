//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using System.Linq;
//using WorkFinder.Models;


//public class AccountController : Controller
//{
//    private readonly HhandDbContext _context;

//    public AccountController(HhandDbContext context)
//    {
//        _context = context;
//    }

//    //Registration purpose
//    [HttpGet]
//    public IActionResult Register()
//    {
//        return View();
//    }


//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Register(RegisterViewModel model)
//    {



//        if (!ModelState.IsValid)
//        {
//            TempData["Error"] = "Please fill in all required fields.";
//            return View(model);
//        }

//        if (model.IsWorker && (string.IsNullOrEmpty(model.Age) || string.IsNullOrEmpty(model.Salary) || string.IsNullOrEmpty(model.Experienced)))
//        {
//            ModelState.AddModelError("", "Please fill in all worker-specific fields.");
//            return View(model);
//        }

//        if (model.IsWorkProvider && string.IsNullOrEmpty(model.WorkerNeed))
//        {
//            ModelState.AddModelError("", "Please fill in all work provider-specific fields.");
//            return View(model);
//        }

//        try
//        {
//            if (model.IsWorker)
//            {
//                var worker = new WorkerT
//                {
//                    Name = model.Name,
//                    Gender = model.Gender,
//                    Location = model.Location,
//                    Email = model.Email,
//                    Password = model.Password,
//                    Age = model.Age,
//                    Salary = model.Salary,
//                    Experienced = model.Experienced,
//                    ContactNumber = model.ContactNumber 

//};

//                await _context.WorkerTs.AddAsync(worker);
//            }
//            else if (model.IsWorkProvider)
//            {
//                var provider = new WorkProvider
//                {
//                    Name = model.Name,
//                    Gender = model.Gender,
//                    Location = model.Location,
//                    Email = model.Email,
//                    Password = model.Password,
//                    WorkerNeed = model.WorkerNeed,
//                    ContactNumber= model.ContactNumber
//                };

//                await _context.WorkProviders.AddAsync(provider);
//            }
//            else
//            {
//                ModelState.AddModelError("", "Invalid role selected.");
//                return View(model);
//            }

//            await _context.SaveChangesAsync();
//            TempData["Success"] = "Registration successful!";
//            return RedirectToAction("Login", "Account");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Registration error: {ex.Message}");
//            ModelState.AddModelError("", "An error occurred while saving data.");
//            return View(model);
//        }
//    }





//    //Registration purpose ends


//    //Login purpose

//    [HttpGet]
//    public IActionResult Login()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Login(LoginViewModel model)
//    {


//        if (ModelState.IsValid)
//        {
//            if (model.Role == "Admin")
//            {
//                if (model.Email == "admin@example.com" && model.Password == "123")
//                {
//                    HttpContext.Session.SetString("UserRole", "Admin");
//                    HttpContext.Session.SetString("UserName", "Admin");
//                    return RedirectToAction("Dashboard", "Admin");
//                }
//            }
//            else if (model.Role == "Worker")
//            {
//                var worker = _context.WorkerTs
//                    .FirstOrDefault(w => w.Email == model.Email && w.Password == model.Password);

//                if (worker != null)
//                {
//                    HttpContext.Session.SetString("UserRole", "Worker");
//                    HttpContext.Session.SetString("UserName", worker.Name); // 👈 Store name
//                    HttpContext.Session.SetInt32("UserId", worker.Id);
//                    return RedirectToAction("Dashboard", "wEditProfile");
//                }
//            }
//            else if (model.Role == "WorkProvider")
//            {
//                var provider = _context.WorkProviders
//                    .FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);

//                if (provider != null)
//                {
//                    HttpContext.Session.SetString("UserRole", "WorkProvider");
//                    HttpContext.Session.SetString("UserName", provider.Name); // 👈 Store name
//                    HttpContext.Session.SetInt32("UserId", provider.Id);
//                    return RedirectToAction("Dashboard", "wProviderEditProfile");
//                }
//            }

//            ViewBag.Message = "Invalid credentials or role.";
//        }

//        return View(model);
//    }


//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Logout()
//    {
//        HttpContext.Session.Clear(); // Clears all session data
//        return RedirectToAction("Login", "Account");
//    }

//    public IActionResult AccessDenied()
//    {
//        return View();
//    }


//}


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using WorkFinder.Models;

public class AccountController : Controller
{
    private readonly HhandDbContext _context;

    public AccountController(HhandDbContext context)
    {
        _context = context;
    }

    // Registration
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fill in all required fields.";
            return View(model);
        }

        if (model.IsWorker && (string.IsNullOrEmpty(model.Age) || string.IsNullOrEmpty(model.Salary) || string.IsNullOrEmpty(model.Experienced)))
        {
            ModelState.AddModelError("", "Please fill in all worker-specific fields.");
            return View(model);
        }

        if (model.IsWorkProvider && string.IsNullOrEmpty(model.WorkerNeed))
        {
            ModelState.AddModelError("", "Please fill in all work provider-specific fields.");
            return View(model);
        }

        try
        {
            if (model.IsWorker)
            {
                var worker = new WorkerT
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Location = model.Location,
                    Email = model.Email,
                    Password = model.Password,
                    Age = model.Age,
                    Salary = model.Salary,
                    Experienced = model.Experienced,
                    ContactNumber = model.ContactNumber
                };

                await _context.WorkerTs.AddAsync(worker);
            }
            else if (model.IsWorkProvider)
            {
                var provider = new WorkProvider
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Location = model.Location,
                    Email = model.Email,
                    Password = model.Password,
                    WorkerNeed = model.WorkerNeed,
                    ContactNumber = model.ContactNumber
                };

                await _context.WorkProviders.AddAsync(provider);
            }
            else
            {
                ModelState.AddModelError("", "Invalid role selected.");
                return View(model);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Login", "Account");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration error: {ex.Message}");
            ModelState.AddModelError("", "An error occurred while saving data.");
            return View(model);
        }
    }

    // Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Role == "Admin")
            {
                if (model.Email == "admin@example.com" && model.Password == "123")
                {
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserName", "Admin");
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
            else if (model.Role == "Worker")
            {
                var worker = _context.WorkerTs
                    .FirstOrDefault(w => w.Email == model.Email && w.Password == model.Password);

                if (worker != null)
                {
                    HttpContext.Session.SetString("UserRole", "Worker");
                    HttpContext.Session.SetString("UserName", worker.Name?.ToString() ?? "Worker"); // FIXED
                    HttpContext.Session.SetInt32("UserId", worker.Id);
                    return RedirectToAction("Dashboard", "wEditProfile");
                }
            }
            else if (model.Role == "WorkProvider")
            {
                var provider = _context.WorkProviders
                    .FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);

                if (provider != null)
                {
                    HttpContext.Session.SetString("UserRole", "WorkProvider");
                    HttpContext.Session.SetString("UserName", provider.Name?.ToString() ?? "Provider"); // FIXED
                    HttpContext.Session.SetInt32("UserId", provider.Id);
                    return RedirectToAction("Dashboard", "wProviderEditProfile");
                }
            }

            ViewBag.Message = "Invalid credentials or role.";
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}



