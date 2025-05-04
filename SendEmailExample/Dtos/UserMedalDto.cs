namespace SendEmailExample.Dtos
{
	public class UserMedalDto
	{
		public Guid MedalId { get; set; }
		public string MedalName { get; set; }
		public string IconUrl { get; set; }
		public DateTime AssignedAt { get; set; }
	}
}
