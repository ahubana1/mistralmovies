using Movies_Mistral.Helpers;
using Movies_Mistral.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Services
{
    public interface ISearchService
    {
        MovieQueryResult GetMovies(SearchFilter filter);
        ShowQueryResult GetShows(SearchFilter filter);
    }
}
