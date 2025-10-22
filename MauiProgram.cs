using Microsoft.Extensions.Logging;
using SisCras.ViewModels;
using SisCras.Repositories;
using SisCras.Services;
using SisCras.Database;

namespace SisCras;

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
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		// ViewModels
		builder.Services.AddTransient<PaginaLoginViewModel>();
		builder.Services.AddTransient<PaginaListagemViewModel>();
		builder.Services.AddTransient<PaginaRegistramentoViewModel>();
		builder.Services.AddTransient<PaginaTestesViewModel>();
		builder.Services.AddTransient<PaginaRegTecnicoViewModel>();
		builder.Services.AddScoped<HeaderViewModel>();

		// Services
		builder.Services.AddSingleton<ITecnicoService, TecnicoService>();
		builder.Services.AddSingleton<ILoggedUserService, LoggedUserService>();
		builder.Services.AddSingleton<IProntuarioService, ProntuarioService>();
		builder.Services.AddSingleton<IFamiliaService, FamiliaService>();
		builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
		builder.Services.AddSingleton<ICrasService, CrasService>();
		builder.Services.AddSingleton<IPasswordService, PasswordService>();

		// Repositories
		builder.Services.AddTransient<ITecnicoRepository, TecnicoRepository>();
		builder.Services.AddTransient<IProntuarioRepository, ProntuarioRepository>();
		builder.Services.AddTransient<IFamiliaRepository, FamiliaRepository>();
		builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
		builder.Services.AddTransient<ICrasRepository, CrasRepository>();

		// Database
		builder.Services.AddDbContext<SisCrasDbContext>();

		return builder.Build();
	}
}
