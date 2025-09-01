using Domain.Command;
using Domain.Interfaces;
using MediatR;

namespace Domain.CommandHandlers.Authenticate;

public class GenerateCaptchaCommandHandler : IRequestHandler<GenerateCaptchaCommand, string>
{
    private readonly ICaptchaService _captchaService;

    public GenerateCaptchaCommandHandler(ICaptchaService captchaService)
    {
        _captchaService = captchaService;
    }

    public async Task<string> Handle(GenerateCaptchaCommand request, CancellationToken cancellationToken)
    {
        return _captchaService.GenerateCaptchaCode(request.CaptchaId);
    }
}
