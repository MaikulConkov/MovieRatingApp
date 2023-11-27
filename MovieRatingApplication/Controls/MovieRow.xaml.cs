using Microsoft.Maui.ApplicationModel.DataTransfer;
using MovieRatingApplication.Models;
using System.Windows.Input;

namespace MovieRatingApplication.Controls;

public class MediaSelectedArgs : EventArgs
{
    public Media Media { get; set; }
    public MediaSelectedArgs(Media media)
    {
        Media = media;
    }
}

public partial class MovieRow : ContentView
{
    public static readonly BindableProperty HeadingProperty =
             BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow), string.Empty);

    public static readonly BindableProperty MediasProperty =
            BindableProperty.Create(nameof(Medias), typeof(IEnumerable<Media>), typeof(MovieRow), Enumerable.Empty<Media>());

    public event EventHandler<MediaSelectedArgs> OnMediaSelected;

    public MovieRow()
	{
		InitializeComponent();
        MediaDetailCommand = new Command(ExecuteMediaDetailCommand);
    }

    public string Heading
    {
        get => (string)GetValue(MovieRow.HeadingProperty);
        set => SetValue(MovieRow.HeadingProperty, value);
    }
    public IEnumerable<Media> Medias
    {
        get => (IEnumerable<Media>)GetValue(MovieRow.MediasProperty);
        set => SetValue(MovieRow.MediasProperty, value);
    }

    public ICommand MediaDetailCommand { get; set; }

    private void ExecuteMediaDetailCommand(object parameter)
    {
        if(parameter is Media media && media is not null)
        {
            OnMediaSelected?.Invoke(this, new MediaSelectedArgs(media));
        }
    }
}