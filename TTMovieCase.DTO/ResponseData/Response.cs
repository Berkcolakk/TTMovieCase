using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieCase.Entities.Movie;
using TTMovieCase.Entities.Search;

namespace TTMovieCase.Entities.ResponseData
{
    public class Response
    {
        public Response()
        {
            results = new List<MovieVM>();
        }
        public int? page { get; set; }
        public List<MovieVM> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public List<SearchListVM>? keywords { get; set; }
    }
}
