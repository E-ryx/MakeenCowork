using System.Security.Claims;
using Domain.Command;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddReserve(AddReservationCommand addReservationCommand)
        {
            var UserId = int.Parse(User.Claims.FirstOrDefault().Value);
            addReservationCommand.UserId = UserId;
            if (ModelState.IsValid)
            {

                return Ok(await _mediator.Send(addReservationCommand));
            }

            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> CancleReserve(CancelReserveCommand command)
        {
            var UserId = int.Parse(User.Claims.FirstOrDefault().Value);
            command.UserId = UserId;
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservation([FromQuery]GetAllReservationCommand command)
        {

            return Ok(await _mediator.Send(command)) ;
        }
    }
}
