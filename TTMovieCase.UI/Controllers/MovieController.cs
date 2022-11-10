using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TTMovieCase.Entities.Movie;
using TTMovieCase.Entities.ResponseData;
using TTMovieCase.Entities.Search;
using TTMovieCase.Services.MovieServices;
using TTMovieCase.UI.Models;
using TTMovieCase.Utilities.CookieFilters;

namespace TTMovieCase.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly ICookieManager cookieManager;
        public MovieController(IMovieService _movieService, ICookieManager cookieManager)
        {
            this.movieService = _movieService;
            this.cookieManager = cookieManager;
        }
        [HttpGet]
        public IActionResult Index(string movieSearch)
        {
            //The list page will open without searching.
            if (movieSearch is null)
            {
                Response movieVMs = new();
                movieVMs.keywords = cookieManager.GetSearchList();
                return View(movieVMs);
            }
            //the list that will appear according to the searched word only
            else
            {

                List<SearchListVM> keywordList = cookieManager.AddSearchList(movieSearch);
                Response movieVMs = movieService.SearchMovie(new Dictionary<string, string>() { { "query", movieSearch } });
                movieVMs.keywords = keywordList;
                return View(movieVMs);
            }
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException($"{nameof(id)} is not null");
            }
            MovieVM movieVM = movieService.GetMovieById(id);
            movieVM.SimilarMovies = movieService.SimilarMovies(id).results;
            return View(movieVM);
        }

        [HttpGet]
        public IActionResult UpComing()
        {
            Response movieVMs = movieService.UpcomingMovies();
            return View(movieVMs);
        }
    }
}
