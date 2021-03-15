using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies_Mistral.Models
{
    public class Actor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //...

        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Show> Shows { get; set; }
        public IEnumerable<Episode> Episodes { get; set; }
    }
}
