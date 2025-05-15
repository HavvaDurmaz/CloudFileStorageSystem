using AuthenticationAPI.Dtos;

namespace AuthenticationAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<string?> LoginAsync(LoginRequest request);
    }
}
