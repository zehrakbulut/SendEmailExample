using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendEmailExample.Dtos;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserWithMedals(Guid id)
		{
			var user = await _context.Users
		.Include(u => u.UserMedals)
			.ThenInclude(um => um.Medal)
		.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
				return NotFound();

			var result = new AuthorWithMedalsDto
			{
				Id = user.Id,
				Email = user.Email,
				Medals = user.UserMedals?.Select(um => new UserMedalDto
				{
					MedalId = um.Medal.Id,
					MedalName = um.Medal.Name,
					IconUrl = um.Medal.IconUrl,
					AssignedAt = um.AssignedAt
				}).ToList()
			};

			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var requests = _context.Users.ToListAsync();
			return Ok(requests);
		}
	}
}
