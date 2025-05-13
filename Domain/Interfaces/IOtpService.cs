namespace Domain.Interfaces
{
    public interface IOtpService
    {
        string GenerateOtp(string phoneNumber);
        bool ValidateOtp(string phoneNumber, string otpResponse);
    }
}
