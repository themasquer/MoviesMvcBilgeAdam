using System;
using System.Collections.Generic;
using System.Linq;
using _036_MoviesMvcBilgeAdam.Contexts;
using _036_MoviesMvcBilgeAdam.Models;

namespace _036_MoviesMvcBilgeAdam.Services
{
    public class ReviewService
    {
        // AutoMapper: Bir class tipindeki (Review) bir objeyi başka bir class tipine (ReviewModel),
        // hiç özellikleri assign etmeden (Id = r.Id, Content = r.Content, Date = r.Date...)
        // tek bir konfigürasyon yaparak obje dönüştürme işlemlerini yapan kütüphane. https://automapper.org/

        private readonly MoviesContext _db;

        public ReviewService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<ReviewModel> GetQuery()
        {
            try
            {
                return _db.Reviews.OrderBy(r => r.Movie.Name).Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Date = r.Date,
                    Reviewer = r.Reviewer,
                    Rating = r.Rating,
                    MovieId = r.MovieId,
                    Movie = new MovieModel()
                    {
                        Id = r.Movie.Id,
                        Name = r.Movie.Name,
                        ProductionYear = r.Movie.ProductionYear,
                        BoxOfficeReturn = r.Movie.BoxOfficeReturn,
                        Directors = r.Movie.MovieDirectors.Select(md => new DirectorModel()
                        {
                            Id = md.Director.Id,
                            Name = md.Director.Name,
                            Surname = md.Director.Surname,
                            Retired = md.Director.Retired
                        }).ToList()
                    }
                });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void FillAllRatings(ReviewModel review)
        {
            review.AllRatings = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                review.AllRatings.Add(i);
            }
        }
    }
}