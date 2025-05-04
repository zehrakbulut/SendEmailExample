using Microsoft.EntityFrameworkCore;
using SendEmailExample.Models.Tables;

namespace SendEmailExample
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		
		public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Medal> Medals { get; set; } = null!;
		public DbSet<UserMedal> UserMedals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserMedal>()
				.HasKey(um => new { um.UserId, um.MedalId });

			modelBuilder.Entity<UserMedal>()
				.HasOne(um => um.User)
				.WithMany(u => u.UserMedals)
				.HasForeignKey(um => um.UserId);

			modelBuilder.Entity<UserMedal>()
				.HasOne(um => um.Medal)
				.WithMany(m => m.UserMedals)
				.HasForeignKey(um => um.MedalId);
		}
	}
}
