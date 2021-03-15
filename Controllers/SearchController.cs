using Microsoft.AspNetCore.Mvc;
using Movies_Mistral.Helpers;
using Movies_Mistral.Models.DTOs;
using Movies_Mistral.Services;
using System;

namespace Movies_Mistral.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly IRatingService votingService;

        public SearchController(ISearchService searchService, IRatingService votingService)
        {
            this.searchService = searchService;
            this.votingService = votingService;
        }

        [HttpGet]
        [Route("movies")]
        public IActionResult Movies(string q="", int page = 1, int count = 10)
        {
            SearchFilter filter = new SearchFilter(q, page, count);
            MovieQueryResult movies;

            try
            {
                movies = searchService.GetMovies(filter);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest(new ErrorDTO { 
                    Error = true,
                    ErrorMessage = "Page or count cannot be negative."
                });
            }
            catch (Exception)
            {
                return BadRequest(new ErrorDTO
                {
                    Error = true,
                    ErrorMessage = "Unknown error."
                });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("shows")]
        public IActionResult Shows(string q = "", int page = 1, int count = 10)
        {
            SearchFilter filter = new SearchFilter(q, page, count);
            ShowQueryResult shows;

            try
            {
                shows = searchService.GetShows(filter);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest(new ErrorDTO
                {
                    Error = true,
                    ErrorMessage = "Page or count cannot be negative."
                });
            }
            catch (Exception)
            {
                return BadRequest(new ErrorDTO
                {
                    Error = true,
                    ErrorMessage = "Unknown error."
                });
            }

            return Ok(shows);
        }
    }
}
