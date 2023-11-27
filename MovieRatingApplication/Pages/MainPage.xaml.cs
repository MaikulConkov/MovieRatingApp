using MovieRatingApplication.Controls;
using MovieRatingApplication.Models;
using MovieRatingApplication.ViewModels;

namespace MovieRatingApplication.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;
        public string Type { get; set; } = "movie";
        public MainPage(HomeViewModel homeViewModel)
        {
            InitializeComponent();
            _homeViewModel = homeViewModel;
            BindingContext = homeViewModel;
        }
        protected override async void OnAppearing()
        {     
            base.OnAppearing();
            await _homeViewModel.InitializeAsync(Type);
        }

        private void MovieRow_OnMediaSelected(object sender, Controls.MediaSelectedArgs e)
        {
            _homeViewModel.SelectMediaCommand.Execute(e.Media);
        }

        private void MovieInfo_Closed(object sender, EventArgs e)
        {
            _homeViewModel.UnSelectMedia();
        }

        private void MovieInfo_OnMediaRated(object sender, Controls.MediaRatedArgs e)
        {
            _homeViewModel.OnMediaRated(e);
        }
    }
}