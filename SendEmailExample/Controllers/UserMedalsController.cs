using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmailExample.Dtos;
using SendEmailExample.Services;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserMedalsController : ControllerBase
	{
		private readonly IUserMedalService _userMedalService;

		public UserMedalsController(IUserMedalService userMedalService)
		{
			_userMedalService = userMedalService;
		}

		[HttpPost("assign")]
		public async Task<IActionResult> Assign([FromBody] AssignMedalDto assignMedalDto)
		{
			if (assignMedalDto == null)
				return BadRequest("Invalid data.");
			var result = await _userMedalService.AssignMedalAsync(assignMedalDto.UserId, assignMedalDto.MedalId);
			if (!result)
				return BadRequest("Failed to assign medal.");
			return Ok("Medal assigned successfully.");
		}

		[HttpDelete("remove")]
		public async Task<IActionResult> Remove([FromQuery] Guid userId, [FromQuery] Guid medalId)
		{
			if (userId == Guid.Empty || medalId == Guid.Empty)
				return BadRequest("Invalid data.");
			var result = await _userMedalService.RemoveMedalAsync(userId, medalId);
			if (!result)
				return BadRequest("Failed to remove medal.");
			return Ok("Medal removed successfully.");
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetMedals(Guid userId)
		{
			if (userId == Guid.Empty)
				return BadRequest("Invalid user ID.");
			var medals = await _userMedalService.GetMedalsByUserIdAsync(userId);
			if (medals == null || !medals.Any())
				return NotFound("No medals found for this user.");
			return Ok(medals);
		}
	}
}
