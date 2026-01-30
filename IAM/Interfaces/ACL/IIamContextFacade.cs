namespace IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    int GetCurrentUserId();
    string GetCurrentUserFullName();
}