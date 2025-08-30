using System.Security.Claims;
using Domain.Models;
using Domain.Queries;
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
        public async Task<IActionResult> GetUserProfile()
        {
            // Get the user's ID from the claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var user = await _mediator.Send(int.Parse(userId));
            if (user == null) 
                return NotFound("User Not Found");

            return Ok(user);
        }
        [HttpGet("reservations/current")]
        public async Task<IActionResult> GetUserCurrentReservations()
        {
            // Get the user's ID from the claims
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized("User not authenticated.");
            }

            // Parse the ID (if stored as a string)
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var query = new GetUserReservationsQuery
            {
                UserId = userId
            };

            var currentReservations = await _mediator.Send(query);
            if (currentReservations.Count == 0)
                return NotFound("Reservations not Found");
            return Ok(currentReservations);
        }
        [HttpGet("reservations/{state}")]
        public async Task<IActionResult> GetUserReservations([FromQuery] Reservation.ReservationState state)
        {
            
            // Get the user's ID from the claims
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized("User not authenticated.");
            }

            // Parse the ID (if stored as a string)
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var query = new GetUserReservationsQuery
            {
                UserId = userId,
                State = state
            };

            var reservations = await _mediator.Send(query);
            if (reservations.Count == 0)
                return NotFound("Reservations not Found");
            return Ok(reservations);
        }

        [HttpGet("wallets/current-balance")]
        public async Task<IActionResult> GetUserWalletBalance()
        {
            // Get the user's ID from the claims
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized("User not authenticated.");
            }

            // Parse the ID (if stored as a string)
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID format.");
            }
            
            var walletBalance = await _mediator.Send(new GetUserCurrentWalletBalanceQuery()
                { UserId = int.Parse(userIdString) });
            
            if (walletBalance == null)
                return NotFound();
            
            return Ok(walletBalance);
        }
    }
}
