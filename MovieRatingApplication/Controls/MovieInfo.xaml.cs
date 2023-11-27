
using CommunityToolkit.Mvvm.ComponentModel;
using MovieRatingApplication.Models;
using MovieRatingApplication.Models.Data;
using MovieRatingApplication.Models.RepositoryContracts;
using MovieRatingApplication.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MovieRatingApplication.Controls;

public class MediaRatedArgs : EventArgs
{
    public Media Media { get; set; }

    public int Rating { get; set; }
    public MediaRatedArgs(Media media, int rating)
    {
        Media = media;
        Rating = rating;
    }
}
public partial class MovieInfo : ContentView 
{
	public static readonly BindableProperty MediaProperty = BindableProperty.Create(nameof(Media), typeof(Media), typeof(MovieInfo), null);
    public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(int), typeof(MovieInfo), 0);
    public static readonly BindableProperty IsReadOnlyPropery= BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(MovieInfo), false);
    public event EventHandler<MediaRatedArgs> OnMediaRated;
    public event EventHandler Closed;

    public bool IsVisible => Rating == 0;

 
    public MovieInfo()
	{
		InitializeComponent();
        RatedMediaCommand = new Command(ExecuteRatedMedia);
    }
    public ICommand RatedMediaCommand { get; set; }

    public Media Media
	{
		get => (Media)GetValue(MovieInfo.MediaProperty);
		set => SetValue(MovieInfo.MediaProperty, value);
    }
    public bool IsReadOnly
    {
        get => (bool)GetValue(MovieInfo.IsReadOnlyPropery);
        set => SetValue(MovieInfo.IsReadOnlyPropery, value);
    }
    public int Rating
    {
        get => (int)GetValue(MovieInfo.RatingProperty);
        set =>SetValue(MovieInfo.RatingProperty, value);              
    }

   
    private void Button_Clicked(object sender, EventArgs e) => Closed?.Invoke(this, EventArgs.Empty);

    //private void Rated_CLicked(object sender, EventArgs e)
    //{
    //    var rating = Math.Ceiling(movieRating.Value);
    //    _mediaRepository.RateMovie((int)rating, Media);
    //}


    private void ExecuteRatedMedia(object parameter)
    {
        if (parameter is Media media && media is not null)
        {
            var rating = Math.Ceiling(movieRating.Value);
            OnMediaRated?.Invoke(this, new MediaRatedArgs(media, (int)rating));
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var rating = Math.Ceiling(movieRating.Value);
        OnMediaRated.Invoke(this, new MediaRatedArgs(Media, (int)rating));
    }
}