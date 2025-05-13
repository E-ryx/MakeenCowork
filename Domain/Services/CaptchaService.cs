using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mime;
using System.Text;
using Domain.Interfaces;

namespace Domain.Services;

public class CaptchaService : ICaptchaService
{
    private readonly char[] chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
    private readonly IMemoryService _memoryService;

    public CaptchaService(IMemoryService memoryService)
    {
        _memoryService = memoryService;
    }

    public string GenerateCaptchaCode(string captchaId)
    {
        int length = 6;
        var random = new Random();
        var sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[random.Next(chars.Length)]);
        }

        // Save Captcha in cache.
        _memoryService.SetAsync(captchaId, sb.ToString());
        return sb.ToString();
    }

    // public byte[] GenerateCaptchaImage(string captchaCode)
    // {
    //     int width = 150;
    //     int height = 50;
    //     using var bitmap = new Bitmap(width, height);
    //     using var graphics = Graphics.FromImage(bitmap);
    //     graphics.SmoothingMode = SmoothingMode.AntiAlias;
    //     graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    //     graphics.Clear(Color.White);
    //
    //     var random = new Random();
    //     using var font = new MediaTypeNames.Font("Arial", 20, FontStyle.Bold);
    //     using var brush = new SolidBrush(Color.Black);
    //
    //     float x = 10f;
    //     for (int i = 0; i < captchaCode.Length; i++)
    //     {
    //         graphics.ResetTransform();
    //         var angle = random.Next(-10, 10);
    //         graphics.RotateTransform(angle);
    //         brush.Color = Color.FromArgb(
    //             random.Next(50, 150),
    //             random.Next(50, 150),
    //             random.Next(50, 150));
    //
    //         graphics.DrawString(captchaCode[i].ToString(), font, brush, x, 10);
    //         x += 20;
    //     }
    //
    //     for (int i = 0; i < 10; i++)
    //     {
    //         var pen = new Pen(Color.FromArgb(
    //             random.Next(100, 255),
    //             random.Next(100, 255),
    //             random.Next(100, 255)));
    //         var startPoint = new Point(random.Next(width), random.Next(height));
    //         var endPoint = new Point(random.Next(width), random.Next(height));
    //         graphics.DrawLine(pen, startPoint, endPoint);
    //     }
    //
    //     using var ms = new System.IO.MemoryStream();
    //     bitmap.Save(ms, ImageFormat.Png);
    //     return ms.ToArray();
    // }
    public bool ValidateCaptcha(string captchaId, string captchaResponse)
    {
        var captchaCode = _memoryService.GetAsync(captchaId);
        return captchaCode.Result.ToString() == captchaResponse;
    }
}