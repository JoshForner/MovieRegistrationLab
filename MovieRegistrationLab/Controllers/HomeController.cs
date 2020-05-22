using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRegistrationLab.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace MovieRegistrationLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        public List<Movie> movies = new List<Movie>()
        {
          new Movie(1, "UP", "Adventure", new DateTime(2009, 1,1), "Ed Asner", "Pete Doctor"),
          new Movie(2, "Pulp Fiction", "Drama", new DateTime(1994,1,1), "John Travolta, Uma Thurman, Samuel L. Jackson", "Quentin Tarantino"),
          new Movie(3, "Reservoir Dogs", "Thriller", new DateTime(1992,1,1), "Harvey Keitel", "Quentin Tarantino"),
          new Movie(4, "Inception", "Action", new DateTime(2010,1,1), "Leonardo DiCaprio, Ellen Page, Joseph Gordon-Levitt", "Christopher Nolan"),
          new Movie(5, "Alien", "Horror", new DateTime(1979,1,1), "Sigourney Weaver", "Ridley Scott"),
          new Movie(6, "Forest Gump", "Romance", new DateTime(1994,1,1), "Tom Hanks, Robin Wright", "Robert Zemeckis"),
          new Movie(7, "Back to the Future", "Comedy", new DateTime(1985,1,1), "Micheal J. Fox, Christopher Lloyd", "Robert Zemeckis")

        };
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Privacy()
        {
            string movielistjson = JsonSerializer.Serialize(movies);
            HttpContext.Session.SetString("Movies", movielistjson);
            return RedirectToAction("Index");
        }

        public IActionResult MovieReg(Movie a)
        {
            return View(a);
        }

        public IActionResult MovieAdd(Movie a)
        {
            string movie = JsonSerializer.Serialize(a);
            HttpContext.Session.SetString("Movie", movie);
            return View(a);
        }

        public IActionResult Store(Movie a)
        {
            
            return View(a);
        }

        public IActionResult StorePage(Movie a)
        {
            return View(a);
        }
        public IActionResult AddMovieToList() 
        {
            
            Movie movie = JsonSerializer.Deserialize<Movie>(HttpContext.Session.GetString("Movie"));
            string movielist = HttpContext.Session.GetString("Movies") ?? "Nope";
            if(movielist != "Nope")
            {
            movies = JsonSerializer.Deserialize<List<Movie>>(movielist);
            }
            movies.Add(movie);
            string movielistjson = JsonSerializer.Serialize(movies);
            HttpContext.Session.SetString("Movies", movielistjson);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
