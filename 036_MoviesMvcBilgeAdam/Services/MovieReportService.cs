using System;
using System.Globalization;
using System.Linq;
using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Entities;
using _036_MoviesMvcBilgeAdam.Models;

namespace _036_MoviesMvcBilgeAdam.Services
{
    public class MovieReportService
    {
        private readonly MoviesContext _db;

        public MovieReportService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<MovieReportInnerJoinModel> GetInnerJoinQuery()
        {
            try
            {
                //IQueryable<Movie> movieQuery = _db.Movies;
                IQueryable<Movie> movieQuery = _db.Movies.AsQueryable();

                IQueryable<MovieDirector> movieDirectorQuery = _db.MovieDirectors.AsQueryable();
                IQueryable<Director> directorQuery = _db.Directors.AsQueryable();
                IQueryable<Review> reviewQuery = _db.Reviews.AsQueryable();

                IQueryable<MovieReportInnerJoinModel> query = from m in movieQuery
                                                              join md in movieDirectorQuery
                                                                  on m.Id equals md.MovieId
                                                              join d in directorQuery
                                                                  on md.DirectorId equals d.Id
                                                              join r in reviewQuery
                                                                  on m.Id equals r.MovieId
                                                              select new MovieReportInnerJoinModel()
                                                              {
                                                                  MovieName = m.Name,
                                                                  MovieProductionYear = m.ProductionYear,
                                                                  MovieBoxOfficeReturnValue = m.BoxOfficeReturn,

                                                                  // aşağıdaki şekilde veri dönüşüm işlemi yapıldığında Entity Framework hata verir!
                                                                  //MovieBoxOfficeReturn = m.BoxOfficeReturn.HasValue ? m.BoxOfficeReturn.Value.ToString(new CultureInfo("en")) : "",

                                                                  DirectorFullName = d.Name + " " + d.Surname,
                                                                  DirectorRetiredValue = d.Retired,
                                                                  ReviewContent = r.Content,
                                                                  ReviewRatingValue = r.Rating,
                                                                  ReviewReviewer = r.Reviewer,
                                                                  ReviewDateValue = r.Date
                                                              };
                return query;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}