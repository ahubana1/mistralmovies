using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Models
{
    public class Season
    {
        public string Id { get; set; }
        public int SeasonNumber { get; set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}
