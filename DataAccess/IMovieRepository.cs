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
    public interface IMovieRepository
    {
        MovieQueryResult GetMovies(SearchFilter filter);
        void Rate(RateViewModel rateViewModel);
        void SaveChanges();
    }
}
