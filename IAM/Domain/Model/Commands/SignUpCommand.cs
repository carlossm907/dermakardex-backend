using IAM.Domain.Model.ValueObjects;

namespace IAM.Domain.Model.Commands;

public record SignUpCommand(string Username, PasswordHash Password, Roles Role, FullName FullName);