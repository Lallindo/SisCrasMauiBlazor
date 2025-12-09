using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SisCras.ApplicationLayer.Services;
using SisCras.ApplicationLayer;
using SisCras.Domain.Entities;
using SisCras.Infrastructure.Data.Context;
using SisCras.Infrastructure.Interceptors;
using SisCras.Infrastructure.Repositories;
using SisCras.Presentation.Services;
using SisCras.Presentation.ViewModels;

namespace SisCras;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<FamiliaViewModel>();
        builder.Services.AddTransient<RegistroViewModel>();
        builder.Services.AddTransient<TestesViewModel>();
        builder.Services.AddTransient<EditarFamiliaViewModel>();
        builder.Services.AddTransient<AdminViewModel>();
        builder.Services.AddScoped<HeaderViewModel>();
        builder.Services.AddScoped<MainLayoutViewModel>();

        // Services
            // Application
            builder.Services.AddSingleton<ITecnicoService, TecnicoService>();
            builder.Services.AddSingleton<ILoggedUserService, LoggedUserService>();
            builder.Services.AddSingleton<IProntuarioService, ProntuarioService>();
            builder.Services.AddSingleton<IFamiliaService, FamiliaService>();
            builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
            builder.Services.AddSingleton<ICrasService, CrasService>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            // Presentation
            builder.Services.AddSingleton<IMaskService, MaskService>();

        // Repositories
        builder.Services.AddScoped<ITecnicoRepository, TecnicoRepository>();
        builder.Services.AddScoped<IProntuarioRepository, ProntuarioRepository>();
        builder.Services.AddScoped<IFamiliaRepository, FamiliaRepository>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<ICrasRepository, CrasRepository>();

        // Interceptors
        builder.Services.AddSingleton<TrocaResponsavelInterceptor>();
        
        // Database
        builder.Services.AddDbContext<SisCrasDbContext>((sp, options) =>
        {
            options.UseSqlite($"Data Source={SisCrasDbContext.GetSqLiteConnection()}");
            var responsavelInterceptor =
                sp.GetService<TrocaResponsavelInterceptor>() ?? new TrocaResponsavelInterceptor();
            options.AddInterceptors(responsavelInterceptor);
        });

        return builder.Build();
    }
}
