using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieCase.Entities.Movie;
using TTMovieCase.Entities.ResponseData;

namespace TTMovieCase.Services.MovieServices
{
    public interface IMovieService
    {
        Response SearchMovie(Dictionary<string, string> parameters);
        MovieVM GetMovieById(int id);
        Response SimilarMovies(int id);
        Response UpcomingMovies();
    }
}
