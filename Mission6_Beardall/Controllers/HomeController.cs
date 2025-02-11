using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6_Beardall.Models;

namespace Mission6_Beardall.Controllers
{
    public class HomeController : Controller
    {
        private FilmContext _context;

        public HomeController(FilmContext someName) 
        {
            _context = someName;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Suggestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Suggestion(Movie response)
        {
            _context.Movies.Add(response);
            _context.SaveChanges();
            return View("Thanks", response);
        }
    }
}
