using MediatR;

namespace Domain.Command;

public class GenerateCaptchaCommand: IRequest<string>
{
    public string CaptchaId { get; set; }

}
