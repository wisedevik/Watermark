namespace Watermark.Logger;

internal class ConsoleLogger : ILogger
{
    public void Info(string message) => Console.WriteLine($"[INFO] {message}");
    public void Error(string message) => Console.WriteLine($"[ERROR] {message}");
}
