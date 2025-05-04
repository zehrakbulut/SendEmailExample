using Microsoft.AspNetCore.Mvc;
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
	}
}