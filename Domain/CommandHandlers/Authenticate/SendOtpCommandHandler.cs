using Domain.Command;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers.Authenticate
{
    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, string>
    {
        private readonly IOtpService _otpService;
        private readonly ICaptchaService _captchaService;

        public SendOtpCommandHandler(IOtpService otpService, ICaptchaService captchaService)
        {
            _otpService = otpService;
            _captchaService = captchaService;
        }

        public async Task<string> Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            var captchaValid = _captchaService.ValidateCaptcha(request.CaptchaId, request.CaptchaResponse);
            if (!captchaValid) return "Captcha Not Valid";

            return _otpService.GenerateOtp(request.PhoneNumber);
        }
    }
}
