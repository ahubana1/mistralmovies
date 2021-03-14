using Microsoft.AspNetCore.Mvc;
using Movies_Mistral.Helpers;
using Movies_Mistral.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Movies(string q, int page = 1, int count = 10)
        {
            SearchFilter filter = new SearchFilter(q, page, count);

            return Ok(filter);
        }

        [HttpGet]
        [Route("shows")]
        public IActionResult Shows(string q, int page = 1, int count = 10)
        {
            SearchFilter filter = new SearchFilter(q, page, count);
            return Ok(filter);
        }
    }
}
