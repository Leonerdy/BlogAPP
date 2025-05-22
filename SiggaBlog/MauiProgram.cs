using Microsoft.Extensions.Logging;
using SiggaBlog.Application.UseCases.Posts;
using SiggaBlog.InfraStructure;
using SiggaBlog.ViewModels;
using SiggaBlog.Views;
using SiggaBlog.Domain.Interfaces;
using SiggaBlog.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using SiggaBlog.InfraStructure.Persistence;
using CommunityToolkit.Maui;
using SiggaBlog.Application.UseCases.Comments;
using SiggaBlog.Application;

namespace SiggaBlog;

public static class MauiProgram
{

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var settingsStream = "SiggaBlog.appsettings.json";
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(settingsStream);
        if (stream == null)
        {
            throw new InvalidOperationException($"Could not find embedded resource: {settingsStream}");
        }
        builder.Configuration.AddJsonStream(stream);

        var connectionString = builder.Configuration
            .GetConnectionString("SqliteConnection");
        var directory = FileSystem.Current.AppDataDirectory + "/";
        var options = new DbContextOptionsBuilder<SiggaBlogDbContext>()
            .UseSqlite(string.Format(connectionString!, directory))
            .Options;

        builder.Services.AddInfrastructure(options);
		builder.Services.AddApplication();

		builder.Services.AddTransient<IGetAllPostsUseCase, GetAllPostsUseCase>();
		builder.Services.AddTransient<IGetCommentsByPostIdUseCase, GetCommentsByPostIdUseCase>();
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<PostDetailViewModel>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<PostDetailPage>();
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<INetworkStatus, MauiNetworkStatus>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
