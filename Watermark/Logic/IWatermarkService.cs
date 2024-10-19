namespace Watermark.Logic;

internal interface IWatermarkService
{
    Task AddWatermarkAsync(string imagePath, string watermarkText, WatermarkColor color, float transparency, string fontPath);
}
