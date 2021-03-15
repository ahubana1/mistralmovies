using Movies_Mistral.DataAccess;
using Movies_Mistral.Helpers;
using Movies_Mistral.Models.DTOs;

namespace Movies_Mistral.Services.Implementations
{
    public class SearchService : ISearchService
    {
        public readonly IMovieRepository movieRepository;
        public readonly IShowsRepository showsRepository;

        public SearchService(IMovieRepository movieRepository, IShowsRepository showsRepository)
        {
            this.movieRepository = movieRepository;
            this.showsRepository = showsRepository;
        }

        public MovieQueryResult GetMovies(SearchFilter filter)
        {
            if (!FilterValid(filter)) throw new System.ArgumentOutOfRangeException();

            return movieRepository.GetMovies(filter);
        }

        public ShowQueryResult GetShows(SearchFilter filter)
        {
            if (!FilterValid(filter)) throw new System.ArgumentOutOfRangeException();

            return showsRepository.GetShows(filter);
        }

        private bool FilterValid(SearchFilter filter)
        {
            return filter.Page > 0 && filter.Count > 0;
        }
    }
}
