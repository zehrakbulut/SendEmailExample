using System.Net;
using System.Net.Mail;

namespace SendEmailExample.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetResetPasswordEmailBody(string resetLink)
		{
			var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ResetPasswordTemplate.html");
			var body = File.ReadAllText(templatePath);
			return body.Replace("{{resetLink}}", resetLink);
		}

		public async Task SendEmail(string receptor, string subject, string body, bool isBodyHtml = false)
		{
			var email = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
			var password = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
			var host = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
			var port = _configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

			using var smtpClient = new SmtpClient(host, port)
			{
				EnableSsl = true, 
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(email, password),
				DeliveryMethod = SmtpDeliveryMethod.Network
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(email),
				Subject = subject,
				Body = body,
				IsBodyHtml = isBodyHtml
			};

			mailMessage.To.Add(receptor);

			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
