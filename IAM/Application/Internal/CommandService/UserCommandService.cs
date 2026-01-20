using IAM.Application.Internal.OutboundService;
using IAM.Domain.Model.Aggregates;
using IAM.Domain.Model.Commands;
using IAM.Domain.Repositories;
using IAM.Domain.Services;
using IAM.Domain.Model.Entities;
using dermakardex_backend.Shared.Domain.Repositories;
namespace IAM.Application.Internal.UserCommandService;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash.Value))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var role = Role.FromName(command.Role);
        var user = User.Create(command.FullName, hashedPassword, role, command.Username);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}