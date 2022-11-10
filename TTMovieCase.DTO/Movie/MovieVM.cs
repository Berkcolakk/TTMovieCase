﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieCase.Entities.Movie
{
    public class MovieVM
    {
        public MovieVM(List<MovieVM> similarMovies)
        {
            SimilarMovies = similarMovies;
        }
        public bool adult { get; set; }
        public string? backdrop_path { get; set; }
        public int[]? genre_ids { get; set; }
        public long id { get; set; }
        public string? original_language { get; set; }
        public string? original_title { get; set; }
        public string? overview { get; set; }
        public string? popularity { get; set; }
        public string? poster_path { get; set; }
        public string release_date { get; set; }
        public string? title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public List<MovieVM> SimilarMovies { get; set; }
    }
}