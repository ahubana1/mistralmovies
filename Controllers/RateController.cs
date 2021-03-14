using Microsoft.AspNetCore.Mvc;
using Movies_Mistral.Models.ViewModels;
using Movies_Mistral.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly IRatingService votingService;

        public RateController(ISearchService searchService, IRatingService votingService)
        {
            this.searchService = searchService;
            this.votingService = votingService;
        }

        [HttpPost]
        [Route("movie")]
        public IActionResult RateMovie([FromBody]RateViewModel rateViewModel)
        {
            return Ok("movieID: " + rateViewModel.Id + ", stars: " + rateViewModel.Rate);
        }

        [HttpPost]
        [Route("show")]
        public IActionResult RateShow([FromBody]RateViewModel rateViewModel)
        {
            return Ok("showID: " + rateViewModel.Id + ", stars: " + rateViewModel.Rate);
        }
    }
}
