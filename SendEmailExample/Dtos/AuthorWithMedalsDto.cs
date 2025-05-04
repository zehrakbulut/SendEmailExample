namespace SendEmailExample.Dtos
{
	public class AuthorWithMedalsDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public List<UserMedalDto> Medals { get; set; } = new List<UserMedalDto>();
	}
}
