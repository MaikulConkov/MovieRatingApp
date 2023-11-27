using MovieRatingApplication.Controls;
using MovieRatingApplication.Models;
using MovieRatingApplication.Models.Data;
using MovieRatingApplication.Models.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly AppData _data;

        public MediaRepository(AppData data)
        {
            _data = data;
        }

        public void RateMovie(MediaRatedArgs m)
        {
            _data.RatedMedias.Add(m);
        }

        public async Task<IEnumerable<MediaRatedArgs>> GetAllRatedMedias()
        {
            return _data.RatedMedias;
        }

        public async Task<int> SearchRatedMedia(int mediaId)
        {
            var result = _data.RatedMedias.FirstOrDefault(m => m.Media.Id == mediaId);
            if(result == null)
            {
                return 0;
            }
            else
            {
                return result.Rating;
            }
        }
    }
}
