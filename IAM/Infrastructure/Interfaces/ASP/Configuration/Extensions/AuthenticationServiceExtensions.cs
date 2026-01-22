using System.Text;
using Infrastructure.Tokens.JWT.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tokenSettings = configuration
            .GetSection("TokenSettings")
            .Get<TokenSettings>()
            ?? throw new Exception("TokenSettings not configured");

        var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("ADMIN"));

            options.AddPolicy("UserOnly", policy =>
                policy.RequireRole("USER", "ADMIN"));
        });

        return services;
    }
}