using Microsoft.AspNetCore.Http;
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
			await emailService.SendEmail(receptor, subject, body);
			return Ok();
		}
	}
}
