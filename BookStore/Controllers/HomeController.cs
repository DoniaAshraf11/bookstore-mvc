using BookStore.Models;
using BookStoreMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        ApplicationDbContext context = new ApplicationDbContext(); // ✅ ADD THIS LINE


        public IActionResult Index()
        {
            ViewBag.BookCount = context.Books.Count();
            ViewBag.AuthorCount = context.Authors.Count();
            ViewBag.ReaderCount = context.Readers.Count();
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
