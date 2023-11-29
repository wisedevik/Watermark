using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermark
{
    public class Watermark
    {
        public string UserColor { get; set; }

        public void AddWatermark(string imagePath, string watermarkText, float transparency)
        {
            try
            {
                Bitmap image = new Bitmap(imagePath);

                using (Graphics graphics = Graphics.FromImage(image))
                {

                    Font font = GetFontForImage(image, watermarkText);

                    Color color = Color.White;

                    switch (UserColor)
                    {
                        case "red":
                            color = Color.FromArgb((int)(transparency * 255), 255, 0, 0);
                            break;
                        case "white":
                            color = Color.FromArgb((int)(transparency * 255), 255, 255, 255);
                            break;
                        case "green":
                            color = Color.FromArgb((int)(transparency * 255), 0, 255, 0);
                            break;
                        case "blue":
                            color = Color.FromArgb((int)(transparency * 255), 0, 0, 255);
                            break;
                        case "yellow":
                            color = Color.FromArgb((int)(transparency * 255), 255, 255, 0);
                            break;
                        case "pink":
                            color = Color.FromArgb((int)(transparency * 255), 255, 192, 203);
                            break;
                        case "orange":
                            color = Color.FromArgb((int)(transparency * 255), 255, 165, 0);
                            break;
                        case "magenta":
                            color = Color.FromArgb((int)(transparency * 255), 255, 0, 255);
                            break;
                        case "black":
                            color = Color.FromArgb((int)(transparency * 255), 0, 0, 0);
                            break;
                        default:
                            Console.WriteLine("Color not found!");
                            break;
                    }

                    //Color color = Color.FromArgb((int)(transparency * 255), 255, 255, 255);

                    Brush brush = new SolidBrush(color);

                    float x = 0.0F;
                    float y = (image.Height - font.Size) / 2;

                    graphics.DrawString(watermarkText, font, brush, x, y);
                }

                Console.Write("Enter the output file name (without extension): ");
                string outputFileName = Console.ReadLine();
                image.Save(outputFileName + ".png", ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Debugger.Error($"Error: {ex.Message}");
            }
        }

        public Font GetFontForImage(Bitmap image, string text)
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
    }
}
