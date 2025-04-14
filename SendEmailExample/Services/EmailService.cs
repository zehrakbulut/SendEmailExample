using System.Net;
using System.Net.Mail;

namespace SendEmailExample.Services
{
	public interface IEmailService
	{
		Task SendEmail(string receptor, string subject, string body);
	}

	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmail(string receptor, string subject, string body)
		{
			var email = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
			var password = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
			var host = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
			var port = _configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

			var smtpClient = new SmtpClient(host, port);
			smtpClient.EnableSsl = true;
			smtpClient.UseDefaultCredentials = false;

			smtpClient.Credentials = new NetworkCredential(email, password);

			var message = new MailMessage(email!, receptor, subject, body);
			await smtpClient.SendMailAsync(message);
		}
	}
}
