namespace IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password, string Role, string FullName);