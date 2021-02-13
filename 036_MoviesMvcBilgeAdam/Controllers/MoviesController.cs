﻿using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Entities;
using _036_MoviesMvcBilgeAdam.Models;
using _036_MoviesMvcBilgeAdam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                if (string.IsNullOrWhiteSpace(Name))
                    return Content("Name must not be empty.");
                if (Name.Length > 250)
                    return Content("Name must have maximum 250 characters.");
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

        public ActionResult Details(int? id)
        {
            try
            {
                //if (id == null)
                if (!id.HasValue)
                {
                    //return View("_Exception");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // 400
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Id is required!");
                }
                MovieModel model = _movieService.GetQuery().SingleOrDefault(m => m.Id == id.Value);
                if (model == null)
                {
                    //return View("_Exception");
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound); // 404
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Movie not found!");
                    return HttpNotFound();
                }
                return View(model);
            }
            catch (Exception exc)
            {
                return View("_Exception");
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                MovieModel model = _movieService.GetQuery().SingleOrDefault(m => m.Id == id);
                if (model == null)
                    return HttpNotFound();
                
                List<int> years = new List<int>();
                for (int year = DateTime.Today.Year + 1; year >= 1930; year--)
                {
                    years.Add(year);
                }
                List<SelectListItem> yearSelectListItems = years.Select(y => new SelectListItem()
                {
                    Value = y.ToString(),
                    Text = y.ToString()
                }).ToList();
                SelectList yearSelectList = new SelectList(yearSelectListItems, "Value", "Text", model.ProductionYear);

                //ViewBag.Years = yearSelectList;
                ViewData["Years"] = yearSelectList;

                return View(model);
            }
            catch (Exception exc)
            {
                return View("_Exception");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(string Name, double? BoxOfficeReturn, string ProductionYear)
        public ActionResult Edit(MovieModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _movieService.Update(model);
                    return RedirectToAction("List");
                }

                List<int> years = new List<int>();
                for (int year = DateTime.Now.Date.Year + 1; year >= 1930; year--)
                {
                    years.Add(year);
                }
                List<SelectListItem> yearSelectListItems = years.Select(y => new SelectListItem()
                {
                    Value = y.ToString(),
                    Text = y.ToString()
                }).ToList();
                SelectList yearSelectList = new SelectList(yearSelectListItems, "Value", "Text", model.ProductionYear);
                ViewBag.Years = yearSelectList;

                return View(model);
            }
            catch (Exception exc)
            {
                return View("_Exception");
            }
        }
    }
}