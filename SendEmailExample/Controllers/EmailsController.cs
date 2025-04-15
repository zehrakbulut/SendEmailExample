using Microsoft.AspNetCore.Mvc;
using SendEmailExample.Services;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailsController : ControllerBase
	{
		private readonly IEmailService emailService;

		public EmailsController(IEmailService _emailService)
		{
			emailService = _emailService;
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(string receptor, string subject, string body)
		{
			await emailService.SendEmail(receptor, subject, body,true);
			return Ok("Mail Gönderildi");
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> SendResetPasswordEmail([FromQuery] string email)
		{
			string token = Guid.NewGuid().ToString();
			string resetLink = $"https://niafix.com/reset-password?token={token}";

			var htmlBody = ((EmailService)emailService).GetResetPasswordEmailBody(resetLink);

			await emailService.SendEmail(email, "Şifre Sıfırlama", htmlBody, true);

			return Ok("Şifre sıfırlama e-postası gönderildi.");
		}
	}
}
