﻿using System.Security.Claims;
using AuthenticationAPI.Dtos;
using AuthenticationAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var user = await _authService.GetCurrentUserAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result)

                return BadRequest("Bu e-posta zaten kayıtlı.");
            return Ok("Kullanıcı başarıyla kaydedildi.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);

            if (token is null)
                return Unauthorized("Geçersiz e-posta veya şifre.");

            return Ok(new { Token = token });
        }

        [HttpPost("Refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenResponse = await _authService.RefreshTokenAsync(request.Token);

            if (tokenResponse == null)
                return Unauthorized(new { message = "Geçersiz veya süresi dolmuş refresh token." });

            return Ok(tokenResponse);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request.Token);
            if (!result)
                return BadRequest(new { message = "Geçersiz token veya zaten iptal edilmiş." });

            return Ok(new { message = "Çıkış başarılı." });
        }

        [HttpGet("validate-token")]
        [Authorize] // Token doğrulama için gerekli
        public IActionResult ValidateToken()
        {
            // Token geçerliyse buraya gelir.
            return Ok(new { Valid = true });
        }

    }
}
