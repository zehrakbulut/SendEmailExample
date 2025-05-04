namespace SendEmailExample.Services
{
	public interface IEmailService
	{
		Task SendEmail(string receptor, string subject, string body, bool isBodyHtml = false);
		string GetResetPasswordEmailBody(string resetLink);
	}
}
