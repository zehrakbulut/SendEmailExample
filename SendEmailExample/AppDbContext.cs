using Microsoft.EntityFrameworkCore;
using SendEmailExample.Models.Tables;

namespace SendEmailExample
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		
		public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
