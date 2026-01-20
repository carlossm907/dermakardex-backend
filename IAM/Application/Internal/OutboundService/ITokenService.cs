using IAM.Domain.Model.Aggregates;

namespace IAM.Application.Internal.OutboundService;
public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}