using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Entities;

namespace _036_MoviesMvcBilgeAdam.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Movies
        public ActionResult Index()
        {
            List<Movie> movies = db.Movies.ToList();
            return View(movies);
        }
    }
}