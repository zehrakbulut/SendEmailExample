namespace SendEmailExample.Models.Tables
{
	public class UserMedal
	{
		public Guid UserId { get; set; }
		public User User { get; set; }

		public Guid MedalId { get; set; }
		public Medal Medal { get; set; }

		public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
	}
}
