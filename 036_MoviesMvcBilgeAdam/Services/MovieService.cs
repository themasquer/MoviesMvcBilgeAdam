using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Entities;
using _036_MoviesMvcBilgeAdam.Models;
using System;
using System.Linq;

namespace _036_MoviesMvcBilgeAdam.Services
{
    public class MovieService
    {
        // CRUD: Create, Read, Update, Delete

        //private MoviesContext _db = new MoviesContext(); // _db objesini bu class'ta new'leyip kullanmak yerine Dependency Injection üzerinden dışarıdan alıp kullanmak daha iyi bir yöntem
        private readonly MoviesContext _db;

        public MovieService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<MovieModel> GetQuery()
        {
            try
            {
                return _db.Movies.OrderBy(m => m.Name).Select(m => new MovieModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    BoxOfficeReturn = m.BoxOfficeReturn,
                    ProductionYear = m.ProductionYear,
                    Directors = m.MovieDirectors.Select(md => new DirectorModel()
                    {
                        Id = md.Director.Id,
                        Name = md.Director.Name,
                        Surname = md.Director.Surname,
                        Retired = md.Director.Retired
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewModel()
                    {
                        Id = r.Id,
                        Content = r.Content,
                        Rating = r.Rating,
                        Reviewer = r.Reviewer,
                        MovieId = r.MovieId
                    }).ToList()
                });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Add(MovieModel model)
        {
            try
            {
                Movie entity = new Movie()
                {
                    Name = model.Name,
                    BoxOfficeReturn = model.BoxOfficeReturn,
                    ProductionYear = model.ProductionYear
                };
                _db.Movies.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}