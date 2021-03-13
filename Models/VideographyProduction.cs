using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models
{
    public abstract class VideographyProduction
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CoverImagePath { get; set; }
        public string Plot { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public double Rating { get; set; }
        public int NumberOfRatings { get; set; }

        public IEnumerable<Actor> Actors { get; set; }
    }
}
