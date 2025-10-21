using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SmartHostelManagementSystem.Data;
using SmartHostelManagementSystem.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SmartHostelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public HomeController(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var today = DateTime.UtcNow.DayOfWeek;
            string cacheKey = $"MessMenu_{today}";

            if (!_cache.TryGetValue(cacheKey, out MessMenu? todaysMenu))
            {
                todaysMenu = await _context.MessMenus
                                     .FirstOrDefaultAsync(m => m.DayOfWeek == today);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Today.AddDays(1).AddTicks(-1));
                
                if(todaysMenu != null)
                {
                    _cache.Set(cacheKey, todaysMenu, cacheEntryOptions);
                }
            }

            ViewBag.TodaysMenu = todaysMenu;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}