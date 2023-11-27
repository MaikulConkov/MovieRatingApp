using MovieRatingApplication.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.Models.RepositoryContracts
{
    public interface IMediaRepository
    {
        public void RateMovie(MediaRatedArgs m);
        public  Task<IEnumerable<MediaRatedArgs>> GetAllRatedMedias();
        public  Task<int> SearchRatedMedia(int mediaId);
    }
}
