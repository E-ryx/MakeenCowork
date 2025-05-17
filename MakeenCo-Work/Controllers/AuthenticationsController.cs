using System.Security.Claims;
using Domain.Command;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IUserRepository _userRepository;
        public AuthenticationsController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
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
            if (res == LoginResult.NotFound)
                    return NotFound("User Not Found");
            if (res == LoginResult.WrongOtp)
                    return BadRequest("Wrong Otp");
            var user = await _userRepository.GetUserAsync(command.PhoneNumber);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, new AuthenticationProperties());
            return Ok("User Logged In");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }


    }

}
