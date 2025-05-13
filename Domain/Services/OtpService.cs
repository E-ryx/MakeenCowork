using Domain.Interfaces;

namespace Domain.Services
{
    public class OtpService:IOtpService
    {
        private readonly IMemoryService _memoryService;

        public OtpService(IMemoryService memoryService)
        {
            _memoryService = memoryService;
        }

        public string GenerateOtp(string phoneNumber)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            _memoryService.SetAsync(phoneNumber, otp);
            return otp;
        }
        public bool ValidateOtp(string phoneNumber, string otpResponse)
        {
            var otp = _memoryService.GetAsync(phoneNumber);
            return otp.ToString() == otpResponse;
        }
    }
}
