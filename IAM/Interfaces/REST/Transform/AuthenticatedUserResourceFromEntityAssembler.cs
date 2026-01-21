using System.Linq.Expressions;
using IAM.Domain.Model.Aggregates;
using IAM.Intefaces.REST.Resources;

namespace IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
       return new AuthenticatedUserResource(
            user.Id,
            user.FullName.ToString(),
            user.Username.ToString(),
            user.Role.GetStringName(),
            token
        ); 
    }
}