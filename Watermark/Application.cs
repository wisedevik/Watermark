using Watermark.Logger;
using Watermark.Logic;

namespace Watermark;

internal class Application
{
    private readonly IWatermarkService _watermarkService;
    private readonly ILogger _logger;

    public Application(IWatermarkService watermarkService, ILogger logger)
    {
        _watermarkService = watermarkService;
        _logger = logger;
    }

    public async Task Run()
    {
        Console.Title = "Watermark";
        PrintAscii();

        Console.Write("Enter the text for the watermark: ");
        string watermarkText = Console.ReadLine();

        Console.Write("Enter the text color (Red, White, Green, Blue, Yellow, Pink, Orange, Magenta, Black): ");
        if (!Enum.TryParse<WatermarkColor>(Console.ReadLine(), true, out var color))
        {
            _logger.Error("Invalid color entered.");
            return;
        }

        Console.Write("Enter the path to the image (including the file name): ");
        string imagePath = Console.ReadLine();

        Console.Write("Enter the transparency level (from 0.0 to 1.0): ");
        if (!float.TryParse(Console.ReadLine(), out var transparency))
        {
            _logger.Error("Invalid transparency value.");
            return;
        }

        Console.Write("Enter the path to the font (if you want to use the default font, just write: n): ");
        string fontPath = Console.ReadLine();

        await _watermarkService.AddWatermarkAsync(imagePath, watermarkText, color, transparency, fontPath == "n" ? null : fontPath);

        _logger.Info("Watermark successfully added!");
        Console.Write("\nPress any key to exit the program...");
        Console.ReadKey();
    }

    private void PrintAscii()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"   _____ _                 _       __          __   _                                 _     
  / ____(_)               | |      \ \        / /  | |                               | |    
 | (___  _ _ __ ___  _ __ | | ___   \ \  /\  / /_ _| |_ ___ _ __ _ __ ___   __ _ _ __| | __ 
  \___ \| | '_  _ \| '_ \| |/ _ \   \ \/  \/ / _ | __/ _ \ '__| '_  _ \ / _ | '__| |/ / 
  ____) | | | | | | | |_) | |  __/    \  /\  / (_| | ||  __/ |  | | | | | | (_| | |  |   <  
 |_____/|_|_| |_| |_| .__/|_|\___|     \/  \/ \__,_|\__\___|_|  |_| |_| |_|\__,_|_|  |_|\_\ 
                    | |                                                                     
                    |_|                                                                     
");
        Console.ResetColor();
    }
}
