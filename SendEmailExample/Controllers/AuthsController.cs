using Microsoft.AspNetCore.Mvc;
using SendEmailExample.Dtos;
using SendEmailExample.Services;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthsController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _authService.RegisterAsync(registerDto);
			if(!result.Success)
				return BadRequest(result.Message);

			return Ok(result.Message);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _authService.LoginAsync(loginDto);
			if (!result.Success)
				return BadRequest(result.Message);

			return Ok(result.Message);
		}

		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
		{
			var result = await _authService.SendPasswordResetEmailAsync(dto);
			if (!result.Success)
				return BadRequest(result.Message);

			return Ok(result.Message);
		}


		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
		{
			var result = await _authService.ResetPasswordAsync(resetPasswordDto);
			if (!result.Success)
				return BadRequest(result.Message);

			return Ok(result.Message);
		}
	}
}
