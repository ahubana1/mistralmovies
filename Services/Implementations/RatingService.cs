using Movies_Mistral.DataAccess;
using Movies_Mistral.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Services.Implementations
{
    public class RatingService : IRatingService
    {
        public readonly IMovieRepository movieRepository;
        public readonly IShowsRepository showsRepository;

        public RatingService(IMovieRepository movieRepository, IShowsRepository showsRepository)
        {
            this.movieRepository = movieRepository;
            this.showsRepository = showsRepository;
        }

        public void RateMovie(RateViewModel rateViewModel)
        {
            movieRepository.Rate(rateViewModel);

            movieRepository.SaveChanges();
        }

        public void RateShow(RateViewModel rateViewModel)
        {
            showsRepository.Rate(rateViewModel);

            showsRepository.SaveChanges();
        }
    }
}
