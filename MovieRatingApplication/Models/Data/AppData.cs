using MovieRatingApplication.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.Models.Data
{
    public class AppData
    {
        public List<MediaRatedArgs> RatedMedias { get; set; } = new List<MediaRatedArgs>();

        public List<Media> MoviesTrending { get; set; } = new List<Media>();

        public List<Media> MoviesAction { get; set; } = new List<Media>();

        public List<Media> MoviesAnimation { get; set; } = new List<Media>();

        public List<Media> MoviesCrime { get; set; } = new List<Media>();

        public List<Media> MoviesDrama { get; set; } = new List<Media>();

        public List<Media> TvTrending { get; set; } = new List<Media>();

        public List<Media> TvAction { get; set; } = new List<Media>();

        public List<Media> TvAnimation { get; set; } = new List<Media>();

        public List<Media> TvCrime { get; set; } = new List<Media>();

        public List<Media> TvDrama { get; set; } = new List<Media>();

    }
}
