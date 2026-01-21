using IAM.Domain.Model.ValueObjects;

namespace IAM.Intefaces.REST.Resources;

public record SignInResource(string Username, string Password);