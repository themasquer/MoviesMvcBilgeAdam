using System.Collections.Generic;
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
        public ActionResult Index(int? OnlyMatchedValue = null)
        {
            List<MovieReportInnerJoinModel> innerJoinList = null;
            List<MovieReportLeftOuterJoinModel> leftOuterJoinList = null;

            if ((OnlyMatchedValue ?? 1) == 1) // only matched, inner join
                innerJoinList = movieReportService.GetInnerJoinQuery().ToList();
            else // not only matched, left outer join
                leftOuterJoinList = movieReportService.GetLeftOuterJoinQuery().ToList();

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

            MoviesReportIndexViewModel viewModel = new MoviesReportIndexViewModel()
            {
                InnerJoinList = innerJoinList,
                LeftOuterJoinList = leftOuterJoinList,

                //OnlyMatchedSelectList = new SelectList(onlyMatchedSelectListItems, "Value", "Text", "1")
                OnlyMatchedSelectList = new SelectList(onlyMatchedSelectListItems, "Value", "Text")

                //, OnlyMatchedValue = 1
            };

            //return View(innerJoinList);
            return View(viewModel);
        }
    }
}