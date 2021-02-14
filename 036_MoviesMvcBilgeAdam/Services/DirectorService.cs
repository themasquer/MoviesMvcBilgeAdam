using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Models;
using System.Linq;

namespace _036_MoviesMvcBilgeAdam.Services
{
    public class DirectorService
    {
        private readonly MoviesContext _db;

        public DirectorService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> GetQuery()
        {
            return _db.Directors.OrderBy(d => d.Name).ThenBy(d => d.Surname).Select(d => new DirectorModel()
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                Retired = d.Retired,

                // 1: 2. yöntemi DirectorModel'da
                //FullName = d.Name + " " + d.Surname,

                Movies = d.MovieDirectors.Select(md => new MovieModel()
                {
                    Id = md.Movie.Id,
                    Name = md.Movie.Name,
                    BoxOfficeReturn = md.Movie.BoxOfficeReturn,
                    ProductionYear = md.Movie.ProductionYear
                }).ToList()
            });
        }
    }
}