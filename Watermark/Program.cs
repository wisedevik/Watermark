using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Watermark;

namespace Watermark;
class Program
{
    static void Main()
    {
        Console.Title = "Simple watermark";

        PrintAscii();

        Debugger.Info("Enter the text for the watermark: ");
        string watermarkText = Console.ReadLine();

        Debugger.Info("Enter the path to the image (including the file name): ");
        string imagePath = Console.ReadLine();

        Debugger.Info("Enter the transparency level (from 0.0 to 1.0): ");
        float transparency = float.Parse(Console.ReadLine());

        AddWatermark(imagePath, watermarkText, transparency);

        Debugger.Info("Watermark successfully added!");
        Thread.Sleep(2000);
    }

    static void AddWatermark(string imagePath, string watermarkText, float transparency)
    {
        try
        {
            Bitmap image = new Bitmap(imagePath);

            using (Graphics graphics = Graphics.FromImage(image))
            {
                Font font = GetFontForImage(image, watermarkText);
                Color color = Color.FromArgb((int)(transparency * 255), 255, 255, 255);

                Brush brush = new SolidBrush(color);

                float x = 0.0F;
                float y = (image.Height - font.Size) / 2;

                graphics.DrawString(watermarkText, font, brush, x, y);
            }

            Debugger.Info("Enter the output file name (without extension): ");
            string outputFileName = Console.ReadLine();
            image.Save(outputFileName + ".png", ImageFormat.Png);
        }
        catch (Exception ex)
        {
            Debugger.Error($"Ошибка: {ex.Message}");
        }
    }

    static Font GetFontForImage(Bitmap image, string text)
    {
        Font font = new Font("Arial", 1);

        using (Graphics graphics = Graphics.FromImage(image))
        {
            SizeF textSize = graphics.MeasureString(text, font);

            while (textSize.Width < image.Width && textSize.Height < image.Height)
            {
                font = new Font("Arial", font.Size + 1);
                textSize = graphics.MeasureString(text, font);
            }
        }

        return font;
    }

    static void PrintAscii()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"   _____ _                 _       __          __   _                                 _     
  / ____(_)               | |      \ \        / /  | |                               | |    
 | (___  _ _ __ ___  _ __ | | ___   \ \  /\  / /_ _| |_ ___ _ __ _ __ ___   __ _ _ __| | __ 
  \___ \| | '_ ` _ \| '_ \| |/ _ \   \ \/  \/ / _` | __/ _ \ '__| '_ ` _ \ / _` | '__| |/ / 
  ____) | | | | | | | |_) | |  __/    \  /\  / (_| | ||  __/ |  | | | | | | (_| | |  |   <  
 |_____/|_|_| |_| |_| .__/|_|\___|     \/  \/ \__,_|\__\___|_|  |_| |_| |_|\__,_|_|  |_|\_\ 
                    | |                                                                     
                    |_|                                                                     
");
        Console.ResetColor();
    }
}
