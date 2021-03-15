using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models.DTOs
{
    public class ShowQueryResult
    {
        public int TotalItems { get; set; }
        public List<ShowDTO> Shows { get; set; }

        public ShowQueryResult()
        {
            Shows = new List<ShowDTO>();
        }
    }
}
