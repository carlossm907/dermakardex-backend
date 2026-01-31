using IAM.Application.ACL.Services;
using IAM.Application.Internal.OutboundService;
using IAM.Application.Internal.QueryService;
using IAM.Application.Internal.UserCommandService;
using IAM.Domain.Repositories;
using IAM.Domain.Services;
using IAM.Infrastructure.Persistence.EF.Configuration.Repositories;
using IAM.Interfaces.ACL;
using Infrastructure.Hashing.BCrypt.Services;
using Infrastructure.Tokens.JWT.Configuration;
using Infrastructure.Tokens.JWT.Services;

namespace IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIamContextServices(this WebApplicationBuilder builder)
    {
        // IAM Bounded Context Injection Configuration

        // TokenSettings Configuration

        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IHashingService, HashingService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
    }
}