using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Watermark.Logger;

namespace Watermark.Logic;

internal class WatermarkService : IWatermarkService
{
    private readonly ILogger _logger;

    public WatermarkService(ILogger logger)
    {
        _logger = logger;
    }

    public async Task AddWatermarkAsync(string imagePath, string watermarkText, WatermarkColor color, float transparency, string fontPath)
    {
        try
        {
            using Bitmap image = new Bitmap(imagePath);

            FontFamily fontFamily;

            if (string.IsNullOrEmpty(fontPath))
            {
                fontFamily = new FontFamily("Arial");
            }
            else
            {
                PrivateFontCollection fonts = new PrivateFontCollection();
                fonts.AddFontFile(fontPath);
                if (fonts.Families.Length == 0)
                {
                    _logger.Error("Failed to load the font. Using default font.");
                    fontFamily = new FontFamily("Arial");
                }
                else
                {
                    fontFamily = fonts.Families[0];
                }
            }

            using Graphics graphics = Graphics.FromImage(image);

            Font font = GetFontForImage(image, watermarkText, fontFamily);
            Color brushColor = GetColor(color, transparency);
            using Brush brush = new SolidBrush(brushColor);

            float x = 0.0F;
            float y = (image.Height - font.Size) / 2;

            graphics.DrawString(watermarkText, font, brush, x, y);

            Console.WriteLine("Enter the output file name (without extension): ");
            string outputFileName = Console.ReadLine();
            image.Save(outputFileName + ".png", ImageFormat.Png);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error: {ex.Message}");
        }
    }

    private Font GetFontForImage(Bitmap image, string text, FontFamily fontFamily)
    {
        var font = new Font(fontFamily, 1);
        using var graphics = Graphics.FromImage(image);

        SizeF textSize;
        do
        {
            font = new Font(fontFamily, font.Size + 1);
            textSize = graphics.MeasureString(text, font);
        } while (textSize.Width < image.Width && textSize.Height < image.Height);

        return font;
    }

    private Color GetColor(WatermarkColor color, float transparency)
    {
        int alpha = (int)(transparency * 255);
        return color switch
        {
            WatermarkColor.Red => Color.FromArgb(alpha, 255, 0, 0),
            WatermarkColor.White => Color.FromArgb(alpha, 255, 255, 255),
            WatermarkColor.Green => Color.FromArgb(alpha, 0, 255, 0),
            WatermarkColor.Blue => Color.FromArgb(alpha, 0, 0, 255),
            WatermarkColor.Yellow => Color.FromArgb(alpha, 255, 255, 0),
            WatermarkColor.Pink => Color.FromArgb(alpha, 255, 192, 203),
            WatermarkColor.Orange => Color.FromArgb(alpha, 255, 165, 0),
            WatermarkColor.Magenta => Color.FromArgb(alpha, 255, 0, 255),
            WatermarkColor.Black => Color.FromArgb(alpha, 0, 0, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(color), "Invalid color.")
        };
    }
}
