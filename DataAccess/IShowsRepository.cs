using Movies_Mistral.Helpers;
using Movies_Mistral.Models.DTOs;
using Movies_Mistral.Models.ViewModels;

namespace Movies_Mistral.DataAccess
{
    public interface IShowsRepository
    {
        ShowQueryResult GetShows(SearchFilter filter);
        void Rate(RateViewModel rateViewModel);
        void SaveChanges();
    }
}
