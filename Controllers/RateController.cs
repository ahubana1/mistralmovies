using Microsoft.AspNetCore.Mvc;
using Movies_Mistral.Models.DTOs;
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
        private readonly IRatingService votingService;

        public RateController(IRatingService votingService)
        {
            this.votingService = votingService;
        }

        [HttpPost]
        [Route("movie")]
        public IActionResult RateMovie([FromBody]RateViewModel rateViewModel)
        {
            try
            {
                votingService.RateMovie(rateViewModel);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorDTO { 
                    Error = true,
                    ErrorMessage = "Unknown error."
                });
            }

            return Ok();
        }

        [HttpPost]
        [Route("show")]
        public IActionResult RateShow([FromBody]RateViewModel rateViewModel)
        {
            try
            {
                votingService.RateShow(rateViewModel);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorDTO
                {
                    Error = true,
                    ErrorMessage = "Unknown error."
                });
            }

            return Ok();
        }
    }
}
