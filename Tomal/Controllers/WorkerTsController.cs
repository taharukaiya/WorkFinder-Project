

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tomal.Models;
using System.Threading.Tasks;
using System.Linq;
using WorkFinder.Models;

[RoleAuthorize("Admin")]
public class WorkerTsController : Controller
{
    private readonly WorkFinder.Models.HhandDbContext _context;

    public WorkerTsController(HhandDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.WorkerTs.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var w = await _context.WorkerTs.FirstOrDefaultAsync(p => p.Id == id);
        return w == null ? NotFound() : View(w);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,Location,Salary,Experienced,Email,Password")] WorkerT workerT)
    {
        if (ModelState.IsValid)
        {
            _context.Add(workerT);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(workerT);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var workerT = await _context.WorkerTs.FindAsync(id);
        if (workerT == null) return NotFound();

        return View(workerT);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,Location,Salary,Experienced,ContactNumber,Email,Password")] WorkerT workerT)
    {
        if (id != workerT.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(workerT);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerTExists(workerT.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(workerT);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var workerT = await _context.WorkerTs.FirstOrDefaultAsync(m => m.Id == id);
        if (workerT == null) return NotFound();

        return View(workerT);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var workerT = await _context.WorkerTs.FindAsync(id);
        if (workerT != null)
        {
            _context.WorkerTs.Remove(workerT);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool WorkerTExists(int id)
    {
        return _context.WorkerTs.Any(e => e.Id == id);
    }

    // for linq searching

   



}








