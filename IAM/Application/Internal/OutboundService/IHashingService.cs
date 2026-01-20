namespace IAM.Application.Internal.OutboundService;

public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}