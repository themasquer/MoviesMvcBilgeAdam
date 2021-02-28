using System;
using System.Linq;
using _036_MoviesMvcBilgeAdam.Contexts;
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

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}