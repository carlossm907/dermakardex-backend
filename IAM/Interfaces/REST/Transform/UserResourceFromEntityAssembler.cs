using IAM.Domain.Model.Aggregates;
using IAM.Interfaces.REST.Resources;

namespace IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.Username.ToString(),
            entity.FullName.ToString(),
            entity.Role.GetStringName()
        );
    }
}