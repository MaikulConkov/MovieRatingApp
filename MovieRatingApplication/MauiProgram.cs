using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MovieRatingApplication.Models.Data;
using MovieRatingApplication.Models.RepositoryContracts;
using MovieRatingApplication.Pages;
using MovieRatingApplication.Repositories;
using MovieRatingApplication.ViewModels;
using Syncfusion.Maui.Core.Hosting;

namespace MovieRatingApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient(TmdbRepository.TmdbHttpClientName, httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));
            builder.Services.AddSingleton<ITmdbRepository, TmdbRepository>();
            builder.Services.AddSingleton<IMediaRepository,MediaRepository>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AppData>();
            builder.Services.AddSingleton<RatedMovies>();
            builder.Services.AddSingleton<RatedMoviesViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();

            return builder.Build();
        }
    }
}