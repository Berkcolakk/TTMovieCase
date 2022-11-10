using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TTMovieCase.Entities.Movie;
using TTMovieCase.Entities.ResponseData;
using TTMovieCase.Utilities.CustomRequest;

namespace TTMovieCase.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly IRequestFilter requestFilter;
        private readonly IConfiguration configuration;

        public MovieService(IRequestFilter requestFilter,
         IConfiguration configuration
            )
        {
            this.requestFilter = requestFilter;
            this.configuration = configuration;
        }
        /// <summary>
        /// Dynamically fetches movies that match the parameter.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="customHostName"></param>
        /// <returns></returns>
        public Response GetMovies(Dictionary<string, string> queryString,string customHostName)
        {
            string response = requestFilter.GetRequest(queryString, customHostName);
            Response results = JsonConvert.DeserializeObject<Response>(response);
            for (int i = 0; i < results.results.Count; i++)
            {
                results.results[i].poster_path = $"{configuration.GetSection("UrlForImage").Value}{results.results[i].backdrop_path}";
                results.results[i].backdrop_path = $"{configuration.GetSection("UrlForImage").Value}{results.results[i].backdrop_path}";
            }
            return results;
        }
        /// <summary>
        /// Searches through movies.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Response SearchMovie(Dictionary<string, string> parameters)
        {
            return  GetMovies(parameters, "/search/movie");
        }
        /// <summary>
        /// Returns the movie that matches the id passed in the parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieVM GetMovieById(int id)
        {

            string response = requestFilter.GetRequest(null, "/movie/" + id.ToString());
            MovieVM results = JsonConvert.DeserializeObject<MovieVM>(response);
            results.poster_path = $"{configuration.GetSection("UrlForImage").Value}{results.backdrop_path}";
            results.backdrop_path = $"{configuration.GetSection("UrlForImage").Value}{results.backdrop_path}";
            return results;
        }
        /// <summary>
        /// This query is made with the movieId belonging to a movie that has been accessed to the detail page and it returns the movies that belong to that movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response SimilarMovies(int id)
        {
            return GetMovies(null, $"/movie/{id.ToString()}/similar");
        }
        /// <summary>
        /// The language parameter is given as static and the region parameter as static. Returns the upcoming movies in the (TR) language region.
        /// </summary>
        /// <returns></returns>
        public Response UpcomingMovies()
        {
            return GetMovies(new Dictionary<string, string>() {
                {"language","tr-TR"},
                {"region","TR" }
            }, "/movie/upcoming");
        }
    }
}
