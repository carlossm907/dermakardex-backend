using IAM.Domain.Model.ValueObjects;

namespace IAM.Intefaces.REST.Resources;

public record AuthenticatedUserResource(int Id, string FullName, string Username, string Roles, string Token);
