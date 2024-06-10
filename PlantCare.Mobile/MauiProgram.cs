﻿using Microsoft.Extensions.Logging;
using PlantCare.Mobile.Services;

namespace PlantCare.Mobile
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5174/api/")
            });

            builder.Services.AddScoped(typeof(ApiConnectionService<>));
            builder.Services.AddScoped(typeof(BusinessLogicService));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
