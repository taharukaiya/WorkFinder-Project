
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tomal.Models;
using WorkFinder.Models;

[RoleAuthorize("Admin")]
public class WorkProvidersController : Controller
{
    private readonly HhandDbContext _context;

    public WorkProvidersController(HhandDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.WorkProviders.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var wp = await _context.WorkProviders.FirstOrDefaultAsync(p => p.Id == id);
        return wp == null ? NotFound() : View(wp);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Gender,Location,Email,Password,WorkerNeed,ContactNumber")] WorkProvider wp)
    {
        if (!ModelState.IsValid) return View(wp);
        _context.Add(wp);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var wp = await _context.WorkProviders.FindAsync(id);
        return wp == null ? NotFound() : View(wp);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Location,Email,Password,WorkerNeed,ContactNumber")] WorkProvider wp)
    {
        if (id != wp.Id) return NotFound();
        if (!ModelState.IsValid) return View(wp);

        try
        {
            _context.Update(wp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WorkProviders.Any(e => e.Id == wp.Id)) return NotFound();
            throw;
        }
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var wp = await _context.WorkProviders.FirstOrDefaultAsync(p => p.Id == id);
        return wp == null ? NotFound() : View(wp);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var wp = await _context.WorkProviders.FindAsync(id);
        if (wp != null)
        {
            _context.WorkProviders.Remove(wp);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
