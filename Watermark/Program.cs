using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Watermark;
using Watermark.Logger;
using Watermark.Logic;

namespace Watermark;
class Program
{
    static async Task Main()
    {
        var services = ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Application>();
        await app.Run();
    }

    private static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddTransient<IWatermarkService, WatermarkService>();
        services.AddSingleton<ILogger, ConsoleLogger>();
        services.AddSingleton<Application>();
        return services;
    }

}
