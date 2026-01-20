using IAM.Domain.Model.Entities;
using IAM.Domain.Model.ValueObjects;

namespace IAM.Domain.Model.Commands;

public record SignUpCommand(string Username, string Password, string Role, string FullName);