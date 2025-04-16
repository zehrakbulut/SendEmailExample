using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendEmailExample.Dtos;
using SendEmailExample.Models.Tables;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public AuthsController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAuth()
		{
			var requests = _context.Users.ToList();
			return Ok(requests);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var existingUser = _context.Users.FirstOrDefault(u => u.Email == registerDto.Email);
			if (existingUser != null)
				return BadRequest("Bu email zaten kayıtlı");

			//var hashedPassword = PasswordHasher.HashPassword(registerDto.Password);

			var user = new User
			{
				Email = registerDto.Email,
				//PasswordHash = hashedPassword
				PasswordHash = registerDto.Password
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return Ok("Kayıt Başarılı..");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
			if (user == null)
				return BadRequest("Kullanıcı Bulunamadı");


			if (user.PasswordHash != loginDto.Password)
				return BadRequest("Şifre Yanlış.");

			return Ok("Giriş Başarılı :)");
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
		{
			var resetRequest = await _context.PasswordResetRequests.FirstOrDefaultAsync(r => r.Token == resetPasswordDto.Token && !r.Used && r.Expiration > DateTime.UtcNow);

			if (resetRequest == null)
				return BadRequest("Geçersiz veya süresi geçmiş token.");

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetRequest.Email);
			if (user == null)
				return BadRequest("Kullanıcı bulunamadı.");

			user.PasswordHash = resetPasswordDto.NewPassword;
			resetRequest.Used = true;
			await _context.SaveChangesAsync();
			return Ok("Şifre başarıyla sıfırlandı.");
		}
	}
}
