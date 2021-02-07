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

        // GET: Movies/Create
        //public ActionResult Create()
        public ViewResult Create()
        {
            //return new ViewResult();
            return View();
        }
        public ContentResult GetHtmlContent()
        {
            //return new ContentResult();
            return Content("<b><i>Content result.</i></b>", "text/html");
        }
        public ActionResult GetMoviesXmlContent() // XML döndürme işlemleri genelde bu şekilde yapılmaz!
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
        /*
                    ActionResult
            ViewResult  ContentResult
        */

        // GET: Movies/CreateSave
        //public ActionResult CreateSave()
        //{

        //}
    }
}