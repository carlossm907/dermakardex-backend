using IAM.Domain.Model.ValueObjects;

namespace IAM.Interfaces.REST.Resources;

public record UserResource(int Id, string Username, string FullName, string Role);