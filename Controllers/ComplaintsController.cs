using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHostelManagementSystem.Data;
using SmartHostelManagementSystem.Models;

namespace SmartHostelManagementSystem.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            IQueryable<Complaint> complaintsQuery = _context.Complaints.Include(c => c.Student);

            if (!isAdmin)
            {
                complaintsQuery = complaintsQuery.Where(c => c.StudentId == userId);
            }

            return View(await complaintsQuery.OrderByDescending(c => c.Id).ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var complaint = await _context.Complaints
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null) return NotFound();
            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                 return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] Complaint complaint)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            complaint.StudentId = (int)userId;
            complaint.Status = "Pending";
            ModelState.Remove("Student");

            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
            if (id == null) return NotFound();
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null) return NotFound();
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
            
            var complaintToUpdate = await _context.Complaints.FindAsync(id);
            if (complaintToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Complaint>(complaintToUpdate, "", c => c.Title, c => c.Description, c => c.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaintToUpdate.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(complaintToUpdate);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
            if (id == null) return NotFound();
            var complaint = await _context.Complaints.Include(c => c.Student).FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null) return NotFound();
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "True") return Forbid();
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Id == id);
        }
    }
}