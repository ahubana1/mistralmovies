using System.ComponentModel.DataAnnotations;

namespace Movies_Mistral.Models.ViewModels
{
    public class RateViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rate { get; set; }
    }
}
