namespace SendEmailExample.Models.Tables
{
	public class Medal
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string IconUrl { get; set; } = string.Empty;

		public ICollection<UserMedal> UserMedals { get; set; }
	}
}
