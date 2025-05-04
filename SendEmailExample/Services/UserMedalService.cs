using Microsoft.EntityFrameworkCore;
using SendEmailExample.Dtos;
using SendEmailExample.Models.Tables;

namespace SendEmailExample.Services
{
	public class UserMedalService : IUserMedalService
	{
		private readonly AppDbContext _context;

		public UserMedalService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AssignMedalAsync(Guid userId, Guid medalId)
		{
			var exists = await _context.UserMedals
				.AnyAsync(um => um.UserId == userId && um.MedalId == medalId);
			if (exists)
				return false;

			var userMedal = new UserMedal
			{
				UserId = userId,
				MedalId = medalId
			};

			_context.UserMedals.Add(userMedal);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<List<MedalDto>> GetMedalsByUserIdAsync(Guid userId)
		{
			return await _context.UserMedals
				.Where(um => um.UserId == userId)
				.Select(um => new MedalDto
				{
					Id = um.Medal.Id,
					Name = um.Medal.Name,
					IconUrl = um.Medal.IconUrl
				}).ToListAsync();
		}

		public async Task<bool> RemoveMedalAsync(Guid userId, Guid medalId)
		{
			var userMedal = await _context.UserMedals.FindAsync(userId, medalId);
			if(userMedal == null)
				return false;	

			_context.UserMedals.Remove(userMedal);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
