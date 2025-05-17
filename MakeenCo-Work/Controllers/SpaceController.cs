using Domain.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpaceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> AddSpace(AddSpaceCommand command)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return Ok();
            }

            return BadRequest();
        }

    }
}
