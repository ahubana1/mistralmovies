using Movies_Mistral.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Services
{
    public interface IRatingService
    {
        void RateMovie(RateViewModel rateViewModel);
        void RateShow(RateViewModel rateViewModel);
    }
}
