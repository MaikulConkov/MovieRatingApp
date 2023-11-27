using MovieRatingApplication.Models;
using MovieRatingApplication.Models.Data;
using MovieRatingApplication.Models.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace MovieRatingApplication.Repositories
{
    public class TmdbRepository : ITmdbRepository
    {
        private readonly AppData _data;
        private const string ApiKey = "47ddbc08c365110188b38fd6e3c1bded";
        public const string TmdbHttpClientName = "TmdbClient";
        private readonly IHttpClientFactory _httpCLientFactory;

        public TmdbRepository(AppData data, IHttpClientFactory httpCLientFactory)
        {
            _data = data;
            _httpCLientFactory = httpCLientFactory;
        }
        private HttpClient HttpClient => _httpCLientFactory.CreateClient(TmdbHttpClientName);
        public async Task<IEnumerable<Media>> GetAllActionMovies()
        {
            
                var url = TmdbUrls.Action("movie");
                var actionMoviesCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(actionMoviesCollection, _data.MoviesAction);
            
                return _data.MoviesAction;
        }

        public async Task<IEnumerable<Media>> GetAllActionTv()
        {
            
                var url = TmdbUrls.Action("tv");
                var actionTvCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(actionTvCollection, _data.TvAction);
            
                return _data.TvAction;
        }

        public async Task<IEnumerable<Media>> GetAllAnimationMovies()
        {
            
                var url = TmdbUrls.Animation("movie");
                var animationMovieCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(animationMovieCollection, _data.MoviesAnimation);
            
                return _data.MoviesAnimation;
        }

        public async Task<IEnumerable<Media>> GetAllAnimationTv()
        {
            
                var url = TmdbUrls.Animation("tv");
                var animationTvCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(animationTvCollection, _data.TvAnimation);
            
                return _data.TvAnimation;
        }

        public async Task<IEnumerable<Media>> GetAllCrimeMovies()
        {
            
                var url = TmdbUrls.Crime("movie");
                var crimeMoviesCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(crimeMoviesCollection, _data.MoviesCrime);
            
                return _data.MoviesCrime;
        }

        public async Task<IEnumerable<Media>> GetAllCrimeTv()
        {
            
                var url = TmdbUrls.Crime("tv");
                var tvCrimeCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(tvCrimeCollection, _data.TvCrime);
            
                return _data.TvCrime;
        }

        public async Task<IEnumerable<Media>> GetAllDramaMovies()
        {
            
                var url = TmdbUrls.Drama("movie");
                var moviesDramaCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(moviesDramaCollection, _data.MoviesDrama);
            
                return _data.MoviesDrama;
        }

        public async Task<IEnumerable<Media>> GetAllDramaTv()
        {
            
                var url = TmdbUrls.Drama("tv");
                var tvDramaCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(tvDramaCollection, _data.TvDrama);
            
                return _data.TvDrama;
        }

        public async Task<IEnumerable<Media>> GetAllTrendingMovies()
        {           
                var url = TmdbUrls.Trending("movie");
                var moviesTrendingCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(moviesTrendingCollection, _data.MoviesTrending);
                return _data.MoviesTrending;
        }

        public async Task<IEnumerable<Media>> GetAllTrendingTv()
        {
            
                var url = TmdbUrls.Trending("tv");
                var tvTrendingCollection = await HttpClient.GetFromJsonAsync<Response>($"{url}&api_key={ApiKey}");
                FetchData(tvTrendingCollection, _data.TvTrending);
            
                return _data.TvTrending;
        }

        public void FetchData(Response response, List<Media> datamedia)
        {
            foreach(var r in response.results)
            {
                var result=r.ToMediaObject();
                datamedia.Add(result);
            }
        }

        public static class TmdbUrls
        {
            public static string Trending(string type)
            {
                return $"3/trending/{type}/week?language=en-US";
            }

            public static string Action(string type)
            {
                if (type == "movie")
                {
                    return $"3/discover/{type}?language=en-US&with_genres=28";
                }
                return $"3/discover/{type}?language=en-US&with_genres=10759";
            }

            public static string Animation(string type)
            {
                return $"3/discover/{type}?language=en-US&with_genres=16";

            }

            public static string Crime(string type)
            {
                return $"3/discover/{type}?language=en-US&with_genres=80";
            }

            public static string Drama(string type)
            {
                return $"3/discover/{type}?language=en-US&with_genres=18";
            }
        }
        public class Response
        {
            public int page { get; set; }
            public Result[] results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }
        public class Result
        {
            public string backdrop_path { get; set; }
            public int[] genre_ids { get; set; }
            public int id { get; set; }
            public string original_title { get; set; }
            public string original_name { get; set; }
            public string overview { get; set; }
            public string poster_path { get; set; }
            public string release_date { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public bool video { get; set; }
            public string media_type { get; set; } // "movie" or "tv"
            public string ThumbnailPath => poster_path ?? backdrop_path;
            public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2/{ThumbnailPath}";
            public string ThumbnailSmall => $"https://image.tmdb.org/t/p/w220_and_h330_face/{ThumbnailPath}";
            public string ThumbnailUrl => $"https://image.tmdb.org/t/p/original/{ThumbnailPath}";
            public string DisplayTitle => title ?? name ?? original_title ?? original_name;

            public Media ToMediaObject() =>
                new()
                {
                    Id = id,
                    DisplayTitle = DisplayTitle,
                    MediaType = media_type,
                    Overview = overview,
                    ReleaseDate = release_date,
                    Thumbnail = Thumbnail,
                    ThumbnailSmall = ThumbnailSmall,
                    ThumbnailUrl = ThumbnailUrl
                };
        }
    }
}
