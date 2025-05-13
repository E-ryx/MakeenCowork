using Domain.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            _mediator.Send(command);

            return Ok();
        }


        [HttpPost("generate-captcha")]
        public async Task<IActionResult> GenerateCaptcha([FromBody] GenerateCaptchaCommand command)
        {
            var captchaCode = await _mediator.Send(command);
            return Ok(captchaCode);
        }

        [HttpGet("send-otp")]
        public async Task<IActionResult> GenerateOtp([FromQuery] SendOtpCommand command)
        {
            var otp = await _mediator.Send(command);
            return Ok(otp);
        }

    }
}
