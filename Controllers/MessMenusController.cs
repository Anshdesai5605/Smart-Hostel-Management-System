using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHostelManagementSystem.Data;
using SmartHostelManagementSystem.Models;

namespace SmartHostelManagementSystem.Controllers
{
    public class MessMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MessMenus.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var messMenu = await _context.MessMenus.FirstOrDefaultAsync(m => m.Id == id);
            if (messMenu == null) return NotFound();
            return View(messMenu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,Breakfast,Lunch,Dinner")] MessMenu messMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messMenu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var messMenu = await _context.MessMenus.FindAsync(id);
            if (messMenu == null) return NotFound();
            return View(messMenu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var menuToUpdate = await _context.MessMenus.FindAsync(id);
            if (menuToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<MessMenu>(menuToUpdate, "", m => m.DayOfWeek, m => m.Breakfast, m => m.Lunch, m => m.Dinner))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessMenuExists(menuToUpdate.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menuToUpdate);
        }

        // GET: MessMenus/Delete/5
// This method shows the confirmation page, fixing the 404 error.
public async Task<IActionResult> Delete(int? id)
{
    if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
    
    if (id == null)
    {
        return NotFound();
    }

    var messMenu = await _context.MessMenus
        .FirstOrDefaultAsync(m => m.Id == id);
    if (messMenu == null)
    {
        return NotFound();
    }

    return View(messMenu);
}

// POST: MessMenus/Delete/5
// This method handles the actual deletion after you confirm.
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
    
    var messMenu = await _context.MessMenus.FindAsync(id);
    if (messMenu != null)
    {
        _context.MessMenus.Remove(messMenu);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}

        private bool MessMenuExists(int id)
        {
            return _context.MessMenus.Any(e => e.Id == id);
        }
    }
}