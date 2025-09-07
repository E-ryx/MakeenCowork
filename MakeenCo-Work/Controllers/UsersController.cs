using System.Security.Claims;
using Domain.Command;
using Domain.Models;
using Domain.Queries;
using Domain.Requests;
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
        [HttpPost("wallets/add-balance")]
        public async Task<IActionResult> AddUserWalletBalance()
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

            var command = new AddTransactionRequestCommand()
            {
                UserId = userId
            };
            
            var result = await _mediator.Send(command);
            
            return Ok(result);
        }

        [HttpGet("transactions/history")]
        public async Task<IActionResult> GetUserTransactionsHistory()
        {
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

            var query = new GetUserTransactionsHistoryQuery()
            {
                UserId = userId
            };

            var transactionsHistory = _mediator.Send(query);
            
            return Ok(query);
        }
        
        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        { 
            var userId = User.FindFirstValue("sub") ?? 
                         User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var tickets = await _mediator.Send(new GetUserTicketsQuery()
            {
                UserId = int.Parse(userId)
            });
            
            return Ok(tickets);
        }

        [HttpPost("tickets/create")]
        public async Task<IActionResult> CreateTicket()
        {
            var userId = User.FindFirstValue("sub") ?? 
                         User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _mediator.Send(new CreateTicketCommand()
            {
                UserId = int.Parse(userId)
            });

            return Ok("Ticket Created");
        }
        
        [HttpGet("tickets/{ticketId:int}")]
        public async Task<IActionResult> GetMessages(int ticketId)
        {
            var userId = User.FindFirstValue("sub") ?? 
                         User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _mediator.Send(new GetTicketMessagesQuery()
            {
                TicketId = ticketId,
                UserId = int.Parse(userId)
            });
            
            return Ok(result);
        }
        [HttpPost("tickets/{ticketId:int}")]
        public async Task<IActionResult> AddMessage(
            int ticketId, 
            [FromBody] AddTicketMessageRequest request)
        {
            var userId = User.FindFirstValue("sub") ?? 
                         User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var command = new AddTicketMessageCommand()
            {
                Body = request.Body,
                TicketId = ticketId,
                UserId = int.Parse(userId)
            };
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMessages), new { ticketId }, result);
        }
    }
}
