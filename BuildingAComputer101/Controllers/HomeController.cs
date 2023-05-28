using BuildingAComputer101.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

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

            // Read the visit count from the text file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "visitcount.txt");
            if (System.IO.File.Exists(filePath))
            {
                string countText = System.IO.File.ReadAllText(filePath);
                int.TryParse(countText, out visitCount);
            }

            // Increment the visit count
            visitCount++;

            // Store the updated visit count in the text file
            System.IO.File.WriteAllText(filePath, visitCount.ToString());

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
