using IAM.Domain.Model.ValueObjects;

namespace IAM.Domain.Model.Commands;

public record SignInCommand (string Username, string Password);