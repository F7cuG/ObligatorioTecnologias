using Microsoft.Extensions.Logging;
using System.IO;
using ObligatorioTecnologias.Services; // 👈 Asegurate de tener esta carpeta y el DatabaseService.cs

namespace ObligatorioTecnologias
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //DBcode
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db3");
            builder.Services.AddSingleton(s => new DatabaseService(dbPath));

            return builder.Build();
        }
    }
}
