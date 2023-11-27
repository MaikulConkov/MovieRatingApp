using MovieRatingApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.Models.RepositoryContracts
{
    public interface ITmdbRepository
    {

        public Task<IEnumerable<Media>> GetAllTrendingMovies();

        public Task<IEnumerable<Media>> GetAllActionMovies();

        public Task<IEnumerable<Media>> GetAllAnimationMovies();

        public Task<IEnumerable<Media>> GetAllCrimeMovies();

        public Task<IEnumerable<Media>> GetAllDramaMovies();

        public Task<IEnumerable<Media>> GetAllTrendingTv();

        public Task<IEnumerable<Media>> GetAllActionTv();

        public Task<IEnumerable<Media>> GetAllAnimationTv();

        public Task<IEnumerable<Media>> GetAllCrimeTv();

        public Task<IEnumerable<Media>> GetAllDramaTv();
    }
}
