using Microsoft.EntityFrameworkCore;
using SendEmailExample.Dtos;
using SendEmailExample.Models.Tables;

namespace SendEmailExample.Services
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _context;
		private readonly IEmailService _emailService;

		public AuthService(AppDbContext context, IEmailService emailService)
		{
			_context = context;
			_emailService = emailService;
		}

		public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto)
		{
			var existingUser = await _context.Users
				.FirstOrDefaultAsync(u => u.Email == registerDto.Email);

			if (existingUser != null)
				return (false, "Bu email zaten kayıtlı");

			var user = new User
			{
				Email = registerDto.Email,
				PasswordHash = registerDto.Password
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return (true, "Kayıt Başarılı..");

		}

		public async Task<(bool Success, string Message)> LoginAsync(LoginDto loginDto)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u =>u.Email == loginDto.Email);

			if (user == null)
				return (false, "Kullanıcı Bulunamadı");

			if (user.PasswordHash != loginDto.Password)
				return (false, "Şifre Yanlış.");

			return (true, "Giriş Başarılı :)");
		}

		public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
		{
			var resetRequest = await _context.PasswordResetRequests
				.FirstOrDefaultAsync(r =>r.Token == resetPasswordDto.Token && !r.Used && r.Expiration > DateTime.UtcNow);

			if(resetRequest == null)
				return (false, "Geçersiz veya süresi dolmuş token.");

			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Email == resetRequest.Email);

			if (user == null)
				return (false, "Kullanıcı bulunamadı.");

			user.PasswordHash = resetPasswordDto.NewPassword;
			resetRequest.Used = true;

			await _context.SaveChangesAsync();
			return (true, "Şifre sıfırlama işlemi başarılı.");
		}

		public async Task<(bool Success, string Message)> SendPasswordResetEmailAsync(ForgotPasswordDto forgotPasswordDto)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
			if (user == null)
				return (false, "Kullanıcı bulunamadı.");

			string token = Guid.NewGuid().ToString();
			var resetRequest = new PasswordResetRequest
			{
				Email = forgotPasswordDto.Email,
				Token = token,
				Expiration = DateTime.UtcNow.AddMinutes(15),
				Used = false
			};

			_context.PasswordResetRequests.Add(resetRequest);
			await _context.SaveChangesAsync();

			string resetLink = $"https://exercise.com/reset-password?token={token}";

			var htmlBody = ((EmailService)_emailService).GetResetPasswordEmailBody(resetLink);
			await _emailService.SendEmail(forgotPasswordDto.Email, "Şifre Sıfırlama", htmlBody, true);

			return (true, "Şifre sıfırlama talebi başarıyla gönderildi. Lütfen e-posta adresinizi kontrol edin.");

		}
	}
}
