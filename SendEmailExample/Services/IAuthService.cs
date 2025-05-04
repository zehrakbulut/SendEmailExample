using SendEmailExample.Dtos;

namespace SendEmailExample.Services
{
	public interface IAuthService
	{
		Task<(bool Success, string Message)> RegisterAsync(RegisterDto registerDto);
		Task<(bool Success, string Message)> LoginAsync(LoginDto loginDto);
		Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
		Task<(bool Success, string Message)> SendPasswordResetEmailAsync(ForgotPasswordDto forgotPasswordDto);
	}
}
