using SendEmailExample.Dtos;

namespace SendEmailExample.Services
{
	public interface IUserMedalService
	{
		Task<bool> AssignMedalAsync(Guid userId, Guid medalId);
		Task<bool> RemoveMedalAsync(Guid userId, Guid medalId);
		Task<List<MedalDto>> GetMedalsByUserIdAsync(Guid userId);
	}
}
