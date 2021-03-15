using System;
using System.Collections.Generic;
using System.Text;

namespace Movies_Mistral.Models.DTOs
{
    public class MovieDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public double Rating { get; set; }

        public string[] Actors { get; set; }

    }
}