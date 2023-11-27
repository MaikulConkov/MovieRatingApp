using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieRatingApplication.Controls;
using MovieRatingApplication.Models;
using MovieRatingApplication.Models.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApplication.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ITmdbRepository _tmdbbRepository;
        private readonly IMediaRepository _mediaRepository;

        public string Genre { get; set; } = "movie";
        public  HomeViewModel(ITmdbRepository tmdbbRepository, IMediaRepository mediaRepository)
        {
            _tmdbbRepository = tmdbbRepository;
            _mediaRepository = mediaRepository;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowMovieInfoBox))]
        private Media _selectedMedia;

        [ObservableProperty]
        private Media _trendingNow;

        [ObservableProperty]
        private int _mediaRating;

        public bool ShowMovieInfoBox => SelectedMedia is not null;

        public ObservableCollection<Media> Trending { get; set; } = new();
        public ObservableCollection<Media> Action { get; set; } = new();
        public ObservableCollection<Media> Animation { get; set; } = new();
        public ObservableCollection<Media> Crime { get; set; } = new();
        public ObservableCollection<Media> Drama { get; set; } = new();
        public async Task InitializeAsync(string type)
        {
           
            Task<IEnumerable<Media>> trendingListTask = null;
            Task<IEnumerable<Media>> actionListTask = null;
            Task<IEnumerable<Media>> animationListTask = null;
            Task<IEnumerable<Media>> crimeListTask = null;
            Task<IEnumerable<Media>> dramaListTask = null;
            if(type=="movie")
            {
                 trendingListTask = _tmdbbRepository.GetAllTrendingMovies();
                 actionListTask = _tmdbbRepository.GetAllActionMovies();
                 animationListTask = _tmdbbRepository.GetAllAnimationMovies();
                 crimeListTask = _tmdbbRepository.GetAllCrimeMovies();
                 dramaListTask = _tmdbbRepository.GetAllDramaMovies();

            }
            else
            {
                trendingListTask = _tmdbbRepository.GetAllTrendingTv();
                actionListTask = _tmdbbRepository.GetAllActionTv();
                animationListTask = _tmdbbRepository.GetAllAnimationTv();
                crimeListTask = _tmdbbRepository.GetAllCrimeTv();
                dramaListTask = _tmdbbRepository.GetAllDramaTv();
            }

            var medias = await Task.WhenAll(trendingListTask, actionListTask, animationListTask, crimeListTask, dramaListTask);

            var trendingList = medias[0];
            var actionList = medias[1];
            var animationList = medias[2];
            var crimeList = medias[3];
            var dramaList = medias[4];

 
                TrendingNow = trendingList.OrderBy(t => Guid.NewGuid())
                                          .FirstOrDefault(t =>
                                              !string.IsNullOrWhiteSpace(t.DisplayTitle)
                                              && !string.IsNullOrWhiteSpace(t.Thumbnail));

                SetMediaCollection(trendingList, Trending);
                SetMediaCollection(actionList, Action);
                SetMediaCollection(animationList, Animation);
                SetMediaCollection(crimeList, Crime);
                SetMediaCollection(dramaList, Drama);
            

        }
      
        private static void SetMediaCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
        {
            collection.Clear();
            foreach (var media in medias)
            {
                collection.Add(media);
            }
        }

        [RelayCommand]
        private async void SelectMedia(Media? media = null)
        {
            if (media is not null)
            {
                if (media.Id == SelectedMedia?.Id)
                {
                    media = null;
                }
                else
                {
                    var rating = await _mediaRepository.SearchRatedMedia(media.Id);
                    MediaRating = rating;
                }
            }
            SelectedMedia = media;
        }

        [RelayCommand]
        public void UnSelectMedia()
        {
            SelectedMedia = null;
        }

        [RelayCommand]
        public void OnMediaRated(MediaRatedArgs m)
        {
            _mediaRepository.RateMovie(m);
            UnSelectMedia();
        }
    }
}
