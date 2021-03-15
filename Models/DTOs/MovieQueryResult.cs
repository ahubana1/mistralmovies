using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models.DTOs
{
    public class MovieQueryResult
    {
        public int TotalItems { get; set; }
        public List<MovieDTO> Movies { get; set; }

        public MovieQueryResult()
        {
            Movies = new List<MovieDTO>();
        }
    }
}
