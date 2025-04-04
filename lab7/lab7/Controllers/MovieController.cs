using lab7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace lab7.Controllers
{
    public class MovieController : Controller
    {


        //lab8 where average orderby
        private readonly Lab7Context _context;

        public MovieController(Lab7Context context)
        {
            _context = context;
        }

        // GET: MovieController

        [HttpGet("lab7/{year}")]
        public IActionResult lab7(int year)
        {
            var movies = _context.TMovies.ToList();

            var filteredMovie = new List<TMovie>();
            if (year >= 2000)
            {

                filteredMovie = movies.Where(movie => movie.Released >= 2000).ToList();
            }
            else
            {
                filteredMovie = movies.Where(movie => movie.Released < 2000).ToList();
            }




            return View(filteredMovie);
        }

        public IActionResult Index(string sortOrder, string search="", string genre = "")
        {

            ViewData["search"] = search;
            ViewData["genre"] = genre;


            ViewData["RatingSortOrder"] = string.IsNullOrEmpty(sortOrder) ? "rating_desc" : "";
            ViewData["year"] = string.IsNullOrEmpty(sortOrder) ? "year" : "";

            var movies = _context.TMovies.Include(m => m.TidNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                movies = movies.Where(m => m.Name.Contains(search) || m.Released.ToString().Contains(search));
            }

       

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.TidNavigation.Name == genre);
            }
            switch (sortOrder)
            {
                case "rating_desc":
                    movies = movies.OrderByDescending(m => m.Rating);
                    break;
                case "year":
                    movies = movies.OrderByDescending(m => m.Rating);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Rating);
                    break;
            }

            return View(movies.ToList());
        }






        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
