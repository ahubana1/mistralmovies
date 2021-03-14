using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models.ViewModels
{
    public class RateViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int Rate { get; set; }
    }
}
