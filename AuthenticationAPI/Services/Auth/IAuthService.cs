using AuthenticationAPI.Dtos;
using AuthenticationAPI.Entities;

namespace AuthenticationAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<TokenResponse?> LoginAsync(LoginRequest request);
        Task<TokenResponse?> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
        Task<User?> GetCurrentUserAsync(int userId);

    }
}
