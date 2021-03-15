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
    public class ShowsRepository : IShowsRepository
    {
        private readonly MoviesContext context;

        public ShowsRepository(MoviesContext context)
        {
            this.context = context;
        }

        public ShowQueryResult GetShows(SearchFilter filter)
        {
            var result = new ShowQueryResult();

            IQueryable<Show> showQuery = context.Shows.Include(m => m.Actors).AsNoTracking()
                .Where(
                show =>
                show.Plot.Contains(filter.SearchTerm) ||
                show.Title.Contains(filter.SearchTerm) ||
                show.Actors.Any(actor => (actor.FirstName + " " + actor.LastName).Contains(filter.SearchTerm))
                );

            result.TotalItems = showQuery.Count(); //number of items before pagination

            showQuery = showQuery
                .OrderByDescending(m => m.Rating) //sorting by rating - highest first
                .Skip((filter.Page - 1) * filter.Count) //pagination
                .Take(filter.Count);

            result.Shows = showQuery.Select(s => Mapper(s)).ToList();
            return result;
        }

        private static ShowDTO Mapper(Show show)
        {
            return new ShowDTO
            {
                Id = show.Id,
                Plot = show.Plot,
                Rating = show.Rating,
                Title = show.Title,
                ReleaseDate = show.ReleaseDate,
                Actors = show.Actors.Select(a => (a.FirstName + " " + a.LastName)).ToArray(),
            };
        }

        public void Rate(RateViewModel rateViewModel)
        {
            var show = context.Shows.Find(rateViewModel.Id);

            show.Rating = (show.Rating * show.NumberOfRatings) / (++show.NumberOfRatings);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
