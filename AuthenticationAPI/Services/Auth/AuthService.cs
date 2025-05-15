using System.Security.Cryptography;
using System.Text;
using AuthenticationAPI.Dtos;
using AuthenticationAPI.Entities;
using AuthenticationAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return false; // Email zaten var

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = Role.User
            }; 

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;

            if (VerifyPassword(request.Password, user.PasswordHash))
            {
                // Burada JWT token oluşturacağız, şimdilik dummy string dönelim
                return "token";
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }

    }
}
