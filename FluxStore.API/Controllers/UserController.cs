using FluxStore.Application.Interfaces;
using FluxStore.Application.User.Command;
using FluxStore.Application.User.Handlers;
using FluxStore.Domain.Repositories;
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
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public UserController(IMediator mediator,
            IFileService fileService, IUserRepository userRepository)
        {
            _mediator = mediator;
            _fileService = fileService;
            _userRepository = userRepository;
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

        [HttpPost("update-profile-image")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile? image)
        {
            try
            {
                if (image == null || image.Length == 0)
                    return BadRequest("Image is required.");

                var imageUrl = await _fileService.UploadImageAsync(image);

                // Optional: save to current user
                var user = await _userRepository.GetCurrentUserAsync();
                if (user == null)
                    return NotFound("User not found");

                user.ImageUrl = imageUrl.Replace("api/", "");
                user.UpdatedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                return Ok(new { imageUrl });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}

