using MovieRatingApplication.ViewModels;

namespace MovieRatingApplication.Pages;

public partial class RatedMovies : ContentPage
{
	private readonly RatedMoviesViewModel _ratedMoviesViewModel;
	public RatedMovies(RatedMoviesViewModel ratedMoviesViewModel)
	{
		InitializeComponent();
        _ratedMoviesViewModel = ratedMoviesViewModel;
        BindingContext = ratedMoviesViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _ratedMoviesViewModel.InitializeAsync();
    }
}