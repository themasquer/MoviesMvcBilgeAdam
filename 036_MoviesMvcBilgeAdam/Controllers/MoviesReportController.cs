using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Models;
using _036_MoviesMvcBilgeAdam.Services;

namespace _036_MoviesMvcBilgeAdam.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesReportController : Controller
    {
        private MoviesContext db;
        private MovieReportService movieReportService;

        public MoviesReportController()
        {
            db = new MoviesContext();
            movieReportService = new MovieReportService(db);
        }

        // GET: MoviesReport
        //public ActionResult Index(int? OnlyMatchedValue = null)
        public ActionResult Index(MoviesReportIndexViewModel moviesReport)
        {
            IQueryable<MovieReportInnerJoinModel> innerJoinQuery;
            IQueryable<MovieReportLeftOuterJoinModel> leftOuterJoinQuery;

            List<MovieReportInnerJoinModel> innerJoinList = null;
            List<MovieReportLeftOuterJoinModel> leftOuterJoinList = null;

            //if ((OnlyMatchedValue ?? 0) == 1) // only matched, inner join
            if (moviesReport.OnlyMatchedValue == 1)
            {
                innerJoinQuery = movieReportService.GetInnerJoinQuery();
                if (!string.IsNullOrWhiteSpace(moviesReport.MovieName))
                    innerJoinQuery = innerJoinQuery.Where(model => model.MovieName.ToUpper().Contains(moviesReport.MovieName.ToUpper().Trim()));
                if (moviesReport.ProductionYears != null && moviesReport.ProductionYears.Count > 0)
                    innerJoinQuery = innerJoinQuery.Where(model => moviesReport.ProductionYears.Contains(model.MovieProductionYear));
                double boxOfficeReturn1;
                double boxOfficeReturn2;
                if (!string.IsNullOrWhiteSpace(moviesReport.BoxOfficeReturn1))
                {
                    if (double.TryParse(moviesReport.BoxOfficeReturn1.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out boxOfficeReturn1))
                        innerJoinQuery = innerJoinQuery.Where(model => model.MovieBoxOfficeReturnValue >= boxOfficeReturn1);
                }
                if (!string.IsNullOrWhiteSpace(moviesReport.BoxOfficeReturn2))
                {
                    if (double.TryParse(moviesReport.BoxOfficeReturn2.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out boxOfficeReturn2))
                        innerJoinQuery = innerJoinQuery.Where(model => model.MovieBoxOfficeReturnValue <= boxOfficeReturn2);
                }
                DateTime reviewDate1;
                DateTime reviewDate2;
                if (!string.IsNullOrWhiteSpace(moviesReport.ReviewDate1))
                {
                    reviewDate1 = DateTime.Parse(moviesReport.ReviewDate1, new CultureInfo("en"));
                    innerJoinQuery = innerJoinQuery.Where(model => model.ReviewDateValue >= reviewDate1);
                }
                if (!string.IsNullOrWhiteSpace(moviesReport.ReviewDate2))
                {
                    reviewDate2 = DateTime.Parse(moviesReport.ReviewDate2, new CultureInfo("en"));
                    innerJoinQuery = innerJoinQuery.Where(model => model.ReviewDateValue <= reviewDate2);
                }

                innerJoinList = innerJoinQuery.ToList();
            }
            else // not only matched, left outer join
            {
                leftOuterJoinQuery = movieReportService.GetLeftOuterJoinQuery();
                if (!string.IsNullOrWhiteSpace(moviesReport.MovieName))
                    leftOuterJoinQuery = leftOuterJoinQuery.Where(model => model.MovieName.ToUpper().Contains(moviesReport.MovieName.ToUpper().Trim()));
                if (moviesReport.ProductionYears != null && moviesReport.ProductionYears.Count > 0)
                    leftOuterJoinQuery = leftOuterJoinQuery.Where(model => moviesReport.ProductionYears.Contains(model.MovieProductionYear));
                double boxOfficeReturn1;
                double boxOfficeReturn2;
                if (!string.IsNullOrWhiteSpace(moviesReport.BoxOfficeReturn1))
                {
                    if (double.TryParse(moviesReport.BoxOfficeReturn1.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out boxOfficeReturn1))
                        leftOuterJoinQuery = leftOuterJoinQuery.Where(model => model.MovieBoxOfficeReturnValue >= boxOfficeReturn1);
                }
                if (!string.IsNullOrWhiteSpace(moviesReport.BoxOfficeReturn2))
                {
                    if (double.TryParse(moviesReport.BoxOfficeReturn2.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out boxOfficeReturn2))
                        leftOuterJoinQuery = leftOuterJoinQuery.Where(model => model.MovieBoxOfficeReturnValue <= boxOfficeReturn2);
                }
                DateTime reviewDate1;
                DateTime reviewDate2;
                if (!string.IsNullOrWhiteSpace(moviesReport.ReviewDate1))
                {
                    reviewDate1 = DateTime.Parse(moviesReport.ReviewDate1, new CultureInfo("en"));
                    leftOuterJoinQuery = leftOuterJoinQuery.Where(model => model.ReviewDateValue >= reviewDate1);
                }
                if (!string.IsNullOrWhiteSpace(moviesReport.ReviewDate2))
                {
                    reviewDate2 = DateTime.Parse(moviesReport.ReviewDate2, new CultureInfo("en"));
                    leftOuterJoinQuery = leftOuterJoinQuery.Where(model => model.ReviewDateValue <= reviewDate2);
                }

                leftOuterJoinList = leftOuterJoinQuery.ToList();
            }

            List<SelectListItem> onlyMatchedSelectListItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "1",
                    Text = "Yes"

                    //, Selected = true
                },
                new SelectListItem()
                {
                    Value = "0",
                    Text = "No"
                }
            };

            List<SelectListItem> productionYearSelectListItems = new List<SelectListItem>();
            for (int year = DateTime.Now.Year + 1; year >= 1930; year--)
            {
                productionYearSelectListItems.Add(new SelectListItem()
                {
                    Value = year.ToString(),
                    Text = year.ToString()
                });
            }

            MoviesReportIndexViewModel viewModel = new MoviesReportIndexViewModel()
            {
                InnerJoinList = innerJoinList,
                LeftOuterJoinList = leftOuterJoinList,

                //OnlyMatchedSelectList = new SelectList(onlyMatchedSelectListItems, "Value", "Text", "1")
                OnlyMatchedSelectList = new SelectList(onlyMatchedSelectListItems, "Value", "Text"),

                ProductionYearMultiSelectList = new MultiSelectList(productionYearSelectListItems, "Value", "Text")

                //, OnlyMatchedValue = 1
            };

            //return View(innerJoinList);
            return View(viewModel);
        }
    }
}