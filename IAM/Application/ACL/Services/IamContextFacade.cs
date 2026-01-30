using System.Security.Claims;
using IAM.Interfaces.ACL;

namespace IAM.Application.ACL.Services;

public class IamContextFacade : IIamContextFacade
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IamContextFacade(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserFullName()
    {
        var user = GetUser();

        var fullName = user.FindFirst("fullname")?.Value ?? throw new InvalidOperationException("FullName claim not found.");

        return fullName;
    }

    public int GetCurrentUserId()
    {
        var user = GetUser();

        var userId = user.FindFirst(ClaimTypes.Sid)?.Value ?? throw new InvalidOperationException("UserId claim not found.");

        return int.Parse(userId);
    }

    private ClaimsPrincipal GetUser()
    {
        var context = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No active HttpContext.");

        return context.User
            ?? throw new InvalidOperationException("No authenticated user.");
    }
}