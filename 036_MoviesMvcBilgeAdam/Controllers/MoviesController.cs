using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Entities;
using _036_MoviesMvcBilgeAdam.Models;
using _036_MoviesMvcBilgeAdam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _036_MoviesMvcBilgeAdam.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesContext _db;
        private readonly MovieService _movieService;

        public MoviesController()
        {
            _db = new MoviesContext();
            _movieService = new MovieService(_db);
        }

        // GET: Movies
        public ActionResult Index()
        {
            List<Movie> movies = _db.Movies.ToList();
            return View(movies);
        }

        // GET: Movies/List
        public ActionResult List()
        {
            try
            {
                List<MovieModel> model = _movieService.GetQuery().ToList();
                return View("MovieList", model);
            }
            catch (Exception exc)
            {
                return View("_Exception");
            }
        }

/*
                                                                                ActionResult
ViewResult (View())  ContentResult (Content()) EmptyResult   FileContentResult (File()) HttpResults JavaScriptResult (JavaScript())  JsonResult (Json())   RedirectResults
*/
        public ContentResult GetHtmlContent()
        {
            //return new ContentResult();
            return Content("<b><i>Content result.</i></b>", "text/html");
        }
        public ActionResult GetMoviesXmlContent() // XML döndürme işlemleri genelde bu şekilde yapılmaz, web servisler üzerinden döndürülür!
        {
            List<MovieModel> movies = _movieService.GetQuery().ToList();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            xml += "<MovieModels>";
            foreach (MovieModel movie in movies)
            {
                xml += "<MovieModel>";
                xml += "<Id>" + movie.Id + "</Id>";
                xml += "<Name>" + movie.Name + "</Name>";
                xml += "<ProductionYear>" + movie.ProductionYear + "</ProductionYear>";
                xml += "<BoxOfficeReturn>" + movie.BoxOfficeReturn + "</BoxOfficeReturn>";
                xml += "</MovieModel>";
            }
            xml += "</MovieModels>";
            return Content(xml, "application/xml");
        }
        public string GetString()
        {
            return "String.";
        }
        public EmptyResult GetEmpty()
        {
            return new EmptyResult();
        }
        // GET: Movies/Create
        //public ActionResult Create()
        [HttpGet] // bu action method selector yazılmadığında default'u HttpGet'tir
        public ViewResult Create()
        {
            List<int> years = new List<int>();
            for (int year = DateTime.Now.Year + 1; year >= 1930; year--)
            {
                years.Add(year);
            }
            ViewBag.Years = years;

            //return new ViewResult();
            return View();
        }

        // GET: Movies/CreateSubmit
        //public ActionResult CreateSubmit(string Name, double? BoxOfficeReturn, string ProductionYear)
        //{
        //    return Content("Movie submitted: Name = " + Name + ", BoxOfficeReturn = " + BoxOfficeReturn + ", ProductionYear = " + ProductionYear);
        //}
        [HttpPost]
        public ActionResult Create(string Name, double? BoxOfficeReturn, string ProductionYear)
        {
            try
            {
                MovieModel model = new MovieModel()
                {
                    Name = Name,
                    BoxOfficeReturn = BoxOfficeReturn,
                    ProductionYear = ProductionYear
                };
                _movieService.Add(model);

                //return RedirectToAction("List", "Movies");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                return View("_Exception");
            }
        }
    }
}