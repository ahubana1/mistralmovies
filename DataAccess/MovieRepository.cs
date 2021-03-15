using Microsoft.EntityFrameworkCore;
using Movies_Mistral.Helpers;
using Movies_Mistral.Models;
using Movies_Mistral.Models.DTOs;
using Movies_Mistral.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext context;

        public MovieRepository(MoviesContext context)
        {
            this.context = context;
        }

        public MovieQueryResult GetMovies(SearchFilter filter)
        {
            var result = new MovieQueryResult();

            IQueryable<Movie> movieQuery = context.Movies.Include(m => m.Actors).AsNoTracking()
                .Where(
                movie =>
                movie.Plot.Contains(filter.SearchTerm) ||
                movie.Title.Contains(filter.SearchTerm) ||
                movie.Actors.Any(actor => (actor.FirstName + " " + actor.LastName).Contains(filter.SearchTerm))
                );

            result.TotalItems = movieQuery.Count(); //number of items before pagination

            movieQuery = movieQuery
                .OrderByDescending(m => m.Rating) //sorting by rating - highest first
                .Skip((filter.Page - 1) * filter.Count) //pagination
                .Take(filter.Count);

            result.Movies = movieQuery.Select(s => Mapper(s)).ToList();
            return result;
        }

        private static MovieDTO Mapper(Movie movie)
        {
            return new MovieDTO
            {
                Id = movie.Id,
                Plot = movie.Plot,
                Rating = movie.Rating,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Actors = movie.Actors.Select(a => (a.FirstName + " " + a.LastName)).ToArray(),
            };
        }

        public void Rate(RateViewModel rateViewModel)
        {
            var movie = context.Movies.Find(rateViewModel.Id);

            movie.Rating = (movie.Rating * movie.NumberOfRatings) / (++movie.NumberOfRatings);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
