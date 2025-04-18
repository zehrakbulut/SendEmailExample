﻿using Microsoft.AspNetCore.Mvc;
using SendEmailExample.Models.Tables;
using SendEmailExample.Services;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailsController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IEmailService emailService;

		public EmailsController(IEmailService _emailService, AppDbContext context)
		{
			emailService = _emailService;
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(string receptor, string subject, string body)
		{
			await emailService.SendEmail(receptor, subject, body,true);
			return Ok("Mail Gönderildi");
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> SendResetPasswordEmail([FromQuery] string email, [FromServices] AppDbContext dbContext)
		{
			string token = Guid.NewGuid().ToString();
			var resetRequest = new PasswordResetRequest
			{
				Email = email,
				Token = token,
				Expiration = DateTime.UtcNow.AddMinutes(15)
			};

			dbContext.PasswordResetRequests.Add(resetRequest);
			await dbContext.SaveChangesAsync();

			string resetLink = $"https://exercise.com/reset-password?token={token}";

			var htmlBody = ((EmailService)emailService).GetResetPasswordEmailBody(resetLink);
			await emailService.SendEmail(email, "Şifre Sıfırlama", htmlBody, true);

			return Ok("Şifre sıfırlama e-postası gönderildi.");
		}
	}
}
