using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            // Get the user's ID from the claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }

            // Parse the ID (if stored as a string)
            if (!int.TryParse(userId, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }
            var user = await _mediator.Send(userId);
            if (user == null) 
                return NotFound("User Not Found");

            return Ok(user);
        }
    }
}
