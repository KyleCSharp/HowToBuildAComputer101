using BuildingAComputer101.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HowToBuildPC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            int visitCount = 0;

            // Retrieve the current visit count from cache
            if (_memoryCache.TryGetValue<int>("VisitCount", out int cachedVisitCount))
            {
                visitCount = cachedVisitCount;
            }

            // Increment the visit count
            visitCount++;

            // Store the updated visit count in cache
            _memoryCache.Set("VisitCount", visitCount);

            // Pass the visit count to the view
            ViewBag.VisitCount = visitCount;

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
