using FluxStore.Application.Profile.Queries;
using FluxStore.Application.User.Command;
using FluxStore.Application.User.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _mediator.Send(new GetUserProfileQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var result = await _mediator.Send(new GetProfileQuery(userId));
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }
    }
}

