using Microsoft.Extensions.Logging;
using SiggaBlog.Application.UseCases.Posts;
using SiggaBlog.InfraStructure;
using SiggaBlog.ViewModels;
using SiggaBlog.Views;

namespace SiggaBlog;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "siggaBlog.db");
		builder.Services.AddInfrastructure(dbPath);

		builder.Services.AddScoped<GetAllPostsUseCase>();
		builder.Services.AddScoped<MainPageViewModel>();
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
