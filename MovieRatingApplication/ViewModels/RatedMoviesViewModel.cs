using CommunityToolkit.Mvvm.ComponentModel;
using MovieRatingApplication.Controls;
using MovieRatingApplication.Models.Data;
using MovieRatingApplication.Models.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.ViewModels
{
    public class RatedMoviesViewModel : ObservableObject
    {
        private readonly IMediaRepository _mediaRepository;

        public RatedMoviesViewModel(IMediaRepository mediaRepository)
        {
            _mediaRepository= mediaRepository;
        }

        public ObservableCollection<MediaRatedArgs> RatedMedias { get; set; } = new ();

        public async Task InitializeAsync()
        {
            var ratedMedias = await _mediaRepository.GetAllRatedMedias();
            SetMediaCollection(ratedMedias, RatedMedias);
        }

        private static void SetMediaCollection(IEnumerable<MediaRatedArgs> medias, ObservableCollection<MediaRatedArgs> collection)
        {
            collection.Clear();
            foreach (var media in medias)
            {
                collection.Add(media);
            }
        }
    }
}
