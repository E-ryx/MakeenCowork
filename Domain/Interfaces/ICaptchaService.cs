namespace Domain.Interfaces;

public interface ICaptchaService
{
    string GenerateCaptchaCode(string capthaId);
    // byte[] GenerateCaptchaImage(string captchaCode);
    bool ValidateCaptcha(string captchaId, string captchaResponse);
}
