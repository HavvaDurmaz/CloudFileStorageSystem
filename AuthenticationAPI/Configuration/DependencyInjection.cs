using AuthenticationAPI.Persistence;
using AuthenticationAPI.Services.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(typeof(Program).Assembly);
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
