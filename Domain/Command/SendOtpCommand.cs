using MediatR;

namespace Domain.Command
{
    public class SendOtpCommand : IRequest<string>
    {
        public string PhoneNumber { get; set; }
        public string CaptchaId { get; set; }
        public string CaptchaResponse { get; set; }
    }
}
