using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_Mistral.Helpers
{
    public class SearchFilter
    {
        public string SearchTerm { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }

        public SearchFilter(string searchTerm, int page, int count)
        {
            SearchTerm = searchTerm;
            Page = page;
            Count = count;
        }
    }
}
