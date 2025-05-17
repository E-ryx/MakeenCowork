using System.Security.Claims;
using Domain.Command;
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
        public async Task<IActionResult> AddReser(AddReservationCommand addReservationCommand)
        {
            var UserId = int.Parse(User.Claims.FirstOrDefault().Value);
            addReservationCommand.UserId = UserId;
            if (ModelState.IsValid)
            {
               
                return Ok(await _mediator.Send(addReservationCommand));
            }

            return BadRequest();
        }
    }
}
