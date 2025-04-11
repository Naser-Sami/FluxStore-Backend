using FluxStore.Application.Profile.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FluxStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            var result = await _mediator.Send(new GetProfileQuery(userId));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }
    }
}
