using Domain.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enums.EnumCollection;

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
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Register Failed");
            return Ok("User Successfully Registered.");
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginCommand command)
        {
            var res = await _mediator.Send(command);
                switch (res)
                {
                    case LoginResult.NotFound:
                        ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                        break;
                    case LoginResult.NotActive:
                        ModelState.AddModelError("Email", "کاربر مورد نظر فعال نشده است");
                        break;
                    case LoginResult.WrongPassword:
                        ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                        break;
                    case LoginResult.Succeeded:
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                        };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(principal, new AuthenticationProperties()
                        {
                            IsPersistent = viewModel.RememberMe
                        });
                        TempData["SuccessMessage"] = "خوش آمدید";
                        return RedirectToAction("Index", "Home");
        }
        }

    }
}
