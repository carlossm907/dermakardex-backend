namespace IAM.Domain.Model.Commands;

public record SignInCommand (string Username, string PasswordHash);