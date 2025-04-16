namespace SendEmailExample.Models.Tables
{
	public class User
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
	}
}
