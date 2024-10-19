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
        Console.WriteLine(@" _____ ________  _________ _      _____   _    _  ___ _____ ______________  ___  ___  ______ _   __
/  ___|_   _|  \/  || ___ \ |    |  ___| | |  | |/ _ \_   _|  ___| ___ \  \/  | / _ \ | ___ \ | / /
\ `--.  | | | .  . || |_/ / |    | |__   | |  | / /_\ \| | | |__ | |_/ / .  . |/ /_\ \| |_/ / |/ / 
 `--. \ | | | |\/| ||  __/| |    |  __|  | |/\| |  _  || | |  __||    /| |\/| ||  _  ||    /|    \ 
/\__/ /_| |_| |  | || |   | |____| |___  \  /\  / | | || | | |___| |\ \| |  | || | | || |\ \| |\  \
\____/ \___/\_|  |_/\_|   \_____/\____/   \/  \/\_| |_/\_/ \____/\_| \_\_|  |_/\_| |_/\_| \_\_| \_/
");
        Console.ResetColor();
    }
}
