using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var movies = _context.Movies
                .Include(m => m.Category)  // Load the related Category data
                .ToList();
            return View(movies);
        }

        public IActionResult Collection()
        {
            var movies = _context.Movies
                .Include(m => m.Category)  // Load the related Category data
                .OrderBy(x => x.Director)
                .ToList();
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Suggestion()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                categories = new List<Category> { new Category { CategoryId = 0, CategoryName = "No Categories Available" } };
            }

            // Pass categories to the view
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }



        [HttpPost]
        public IActionResult Suggestion(Movie response)
        {
            if (ModelState.IsValid)
            {
                // Ensure the Category exists and is properly linked
                response.Category = _context.Categories.SingleOrDefault(c => c.CategoryId == response.CategoryId);

                // Save the new movie to the database
                _context.Movies.Add(response);
                _context.SaveChanges();
                return View("Thanks", response);
            }

            // If validation fails, reload the categories for the dropdown
            ViewBag.Categories = _context.Categories.ToList();
            return View(response);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Movies
                .Include(m => m.Category)
                .SingleOrDefault(x => x.MovieID == id);

            if (recordToEdit == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                categories = new List<Category> { new Category { CategoryId = 0, CategoryName = "No Categories Available" } };
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", recordToEdit.CategoryId);

            return View("Suggestion", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie updatedInfo)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", updatedInfo.CategoryId);

            if (!ModelState.IsValid)
            {
                return View("Suggestion", updatedInfo);
            }

            var movieToUpdate = _context.Movies.SingleOrDefault(x => x.MovieID == updatedInfo.MovieID);
            if (movieToUpdate == null)
            {
                return NotFound();
            }

            movieToUpdate.Title = updatedInfo.Title;
            movieToUpdate.CategoryId = updatedInfo.CategoryId;
            movieToUpdate.Year = updatedInfo.Year;
            movieToUpdate.Director = updatedInfo.Director;
            movieToUpdate.Rating = updatedInfo.Rating;
            movieToUpdate.Edited = updatedInfo.Edited;
            movieToUpdate.LentTo = updatedInfo.LentTo;
            movieToUpdate.CopiedToPlex = updatedInfo.CopiedToPlex;
            movieToUpdate.Notes = updatedInfo.Notes;

            _context.Update(movieToUpdate);
            _context.SaveChanges();

            return RedirectToAction("Collection");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Fetch the movie to delete
            var recordToDelete = _context.Movies
                .SingleOrDefault(x => x.MovieID == id);

            if (recordToDelete == null)
            {
                return NotFound();
            }

            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            // Remove the movie from the database
            var movieToDelete = _context.Movies.SingleOrDefault(x => x.MovieID == movie.MovieID);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieToDelete);
            _context.SaveChanges();
            return RedirectToAction("Collection");
        }
    }
}

