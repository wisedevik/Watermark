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
        Console.Title = "Watermark";
        PrintAscii();
        Watermark watermark = new Watermark();

        Console.Write("Enter the text for the watermark: ");
        string watermarkText = Console.ReadLine();

        Console.Write("Red, White, Green, Blue, Yellow, Pink, Orange, Magenta, Black\nEnter the text color: ");
        string textColor = Console.ReadLine().ToLower();
        watermark.UserColor = textColor;

        Console.Write("Enter the path to the image (including the file name): ");
        string imagePath = Console.ReadLine();
        Console.Write("Enter the transparency level (from 0.0 to 1.0, for example, 0.5 corresponds to 50%): ");
        float transparency = float.Parse(Console.ReadLine());

        watermark.AddWatermark(imagePath, watermarkText, transparency);

        Debugger.Info("Watermark successfully added!");
        Console.Write("\nPress any key to exit the program...");
        Console.ReadLine();
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
